using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR

using UnityEditorInternal;
using UnityEditor;

#endif

namespace UnityEditor
{
    // ブラシアセットを作成するタイプのブラシ
    [CustomGridBrush(false, false, false, "Element Brush")]
    public class ElementBrush : GridBrushBase
    {
        // プレハブ
        public GameObject _prefab = null;

        // 要
        [SerializeField]
        public List<Elements> _elements = new List<Elements>();

        // 描画深度
        public int _posZ;

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="brushTarget"></param>
        /// <param name="position"></param>
        public override void Paint(GridLayout grid, GameObject brushTarget, Vector3Int position)
        {
            // 編集を許可しない
            if (brushTarget.layer == 31)
                return;

            var instance = (GameObject)PrefabUtility.InstantiatePrefab(_prefab);

            foreach (var element in _elements)
            {
                var type = System.Type.GetType("Play.Element." + element.ToString());
                instance.AddComponent(type);
            }

            // Ctrl Z できるようにUndo
            Undo.RegisterCreatedObjectUndo((Object)instance, "Paint Prefabs");

            if (instance != null)
            {
                instance.transform.SetParent(brushTarget.transform);
                instance.transform.position = grid.LocalToWorld(grid.CellToLocalInterpolated(new Vector3Int(position.x, position.y, _posZ) + new Vector3(.5f, .5f, .5f)));
            }
        }

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="brushTarget"></param>
        /// <param name="position"></param>
        public override void Erase(GridLayout grid, GameObject brushTarget, Vector3Int position)
        {
            // 編集を許可しない
            if (brushTarget.layer == 31)
                return;

            Transform erased = GetObjectInCell(grid, brushTarget.transform, new Vector3Int(position.x, position.y, _posZ));
            if (erased != null)
                Undo.DestroyObjectImmediate(erased.gameObject);
        }

        /// <summary>
        /// オブジェクトの存在するCellを取得
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="parent"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        private static Transform GetObjectInCell(GridLayout grid, Transform parent, Vector3Int position)
        {
            int childCount = parent.childCount;
            Vector3 min = grid.LocalToWorld(grid.CellToLocalInterpolated(position));
            Vector3 max = grid.LocalToWorld(grid.CellToLocalInterpolated(position + Vector3Int.one));
            Bounds bounds = new Bounds((max + min) * .5f, max - min);

            for (int i = 0; i < childCount; i++)
            {
                Transform child = parent.GetChild(i);
                if (bounds.Contains(child.position))
                    return child;
            }
            return null;
        }

        // １行の大きさ
        private static float GetPerlinValue(Vector3Int position, float scale, float offset)
        {
            return Mathf.PerlinNoise((position.x + offset) * scale, (position.y + offset) * scale);
        }
    }

#if UNITY_EDITOR

    // エディタ
    [CustomEditor(typeof(ElementBrush))]
    public class ElementBrushEditor : GridBrushEditorBase
    {
        private ElementBrush ElementBrush { get { return target as ElementBrush; } }

        private SerializedProperty _prefab;
        private SerializedObject _serializedObject;
        private ReorderableList _enumList;

        protected void OnEnable()
        {
            _serializedObject = new SerializedObject(target);
            _prefab = _serializedObject.FindProperty("_prefab");

            var prop = serializedObject.FindProperty("_elements");
            _enumList = new ReorderableList(serializedObject, prop);

            // ヘッダー
            _enumList.drawHeaderCallback = (rect) =>
                     EditorGUI.LabelField(rect, prop.displayName);

            // 要素の表示
            _enumList.drawElementCallback = (rect, index, isActive, isFocused) =>
            {
                var element = prop.GetArrayElementAtIndex(index);
                rect.height -= 4;
                rect.y += 2;
                EditorGUI.PropertyField(rect, element);
            };
        }

        public override void OnPaintInspectorGUI()
        {
            serializedObject.Update();
            _serializedObject.UpdateIfRequiredOrScript();

            _enumList.DoLayoutList();

            ElementBrush._posZ = EditorGUILayout.IntField("Position Z", ElementBrush._posZ);

            EditorGUILayout.PropertyField(_prefab, true);

            if (GUILayout.Button("要素更新"))
            {
                ElementEnumUpdate();
            }

            _serializedObject.ApplyModifiedPropertiesWithoutUndo();
            serializedObject.ApplyModifiedProperties();
        }

        /// <summary>
        /// 要素の列挙を更新
        /// </summary>
        private void ElementEnumUpdate()
        {
            // 確認するパス
            var outputScriptPath = Directory.GetFiles(Directory.GetCurrentDirectory(), "ElementBase.cs", SearchOption.AllDirectories).FirstOrDefault();
            if (string.IsNullOrEmpty(outputScriptPath))
            {
                throw new System.Exception("要素の列挙を更新できません");
            }

            // フルパスからディレクトリパスにする
            outputScriptPath = System.IO.Path.GetDirectoryName(outputScriptPath);
            // ディレクトリにある要素を全取得
            var files = Util.DirectoryUtils.GetFileNamesInPath(outputScriptPath);

            var names = new List<string>();
            foreach (var path in files)
            {
                // meta 以外の時は追加
                var ex = System.IO.Path.GetExtension(path);
                if (ex == ".meta")
                {
                    continue;
                }

                var name = System.IO.Path.GetFileNameWithoutExtension(path);
                names.Add(name);
            }

            // 保存してenum作成
            var gEnum = new Util.DynamicEnum("Elements");
            if (gEnum.Save(names))
            {
                AssetDatabase.Refresh();
            }
        }
    }
#endif
}
