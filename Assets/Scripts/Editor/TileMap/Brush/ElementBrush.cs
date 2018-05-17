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
    [CustomGridBrush(false, false, false, "Element Brush")]
    public class ElementBrush : LayerObjectBrush<Play.Element.ElementObject>
    {
        // 要素
        [SerializeField]
        public List<Elements> _elements = new List<Elements>();

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="brushTarget"></param>
        /// <param name="position"></param>
        public override void Paint(GridLayout grid, GameObject layer, Vector3Int position)
        {
            var target = GetObject(grid, position);
            if (target != null)
            {
                return;
            }

            var instance = EditorUtil.Instantiate(_prefab, grid.LocalToWorld(grid.CellToLocalInterpolated(position + OffsetFromBottomLeft)), GetLayer());

            foreach (var element in _elements)
            {
                var className = element.ToString();
                var type = Util.TypeUtil.GetTypeByClassName(className);
                // 先頭の子供に要素をつける
                var child = instance.transform.GetChild(0);
                if (child)
                {
                    child.gameObject.AddComponent(type);
                }
            }

            EditorUtil.SetDirty(instance);
            EditorUtil.Select(instance.gameObject);
        }

        /// <summary>
        /// けしけし
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="layer"></param>
        /// <param name="position"></param>
        public override void Erase(GridLayout grid, GameObject layer, Vector3Int position)
        {
            foreach (var obj in AllObjects)
            {
                if (grid.WorldToCell(obj.transform.position) == position)
                {
                    DestroyImmediate(obj.gameObject);

                    EditorUtil.Select(BrushUtil.GetRootGrid(false).gameObject);
                    return;
                }
            }
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
            OnInspectorGUI();

            serializedObject.Update();
            _serializedObject.UpdateIfRequiredOrScript();

            _enumList.DoLayoutList();

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
