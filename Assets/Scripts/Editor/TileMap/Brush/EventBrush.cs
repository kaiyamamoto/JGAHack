using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

#if UNITY_EDITOR

using UnityEditorInternal;
using UnityEditor;

#endif

namespace UnityEditor
{
    [CreateAssetMenu]
    [CustomGridBrush(false, false, false, "Event Brush")]
    public class EventBrush : GridBrushBase
    {
        // イベント
        public MapEvents _event;

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

            var instance = new GameObject(_event.ToString());

            var className = _event.ToString();
            var type = Util.TypeUtil.GetTypeByClassName(className);
            instance.AddComponent(type);

            var rigid = instance.AddComponent<Rigidbody2D>();
            rigid.bodyType = RigidbodyType2D.Static;
            var collider = instance.AddComponent<BoxCollider2D>();
            collider.isTrigger = true;

            // Ctrl Z できるようにUndo
            Undo.RegisterCreatedObjectUndo((Object)instance, "Paint Event");

            if (instance != null)
            {
                // 座標調整
                instance.transform.SetParent(brushTarget.transform);
                instance.transform.position = grid.LocalToWorld(grid.CellToLocalInterpolated(position + new Vector3(.5f, .5f, .5f)));
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

            Transform erased = GetEventInCell(grid, brushTarget.transform, position);
            if (erased != null)
                Undo.DestroyObjectImmediate(erased.gameObject);
        }

        /// <summary>
        /// イベントの存在するCellを取得
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="parent"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        private static Transform GetEventInCell(GridLayout grid, Transform parent, Vector3Int position)
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
    }

#if UNITY_EDITOR

    // エディタ
    [CustomEditor(typeof(EventBrush))]
    public class EventBrushEditor : GridBrushEditorBase
    {
        private EventBrush EventBrush { get { return target as EventBrush; } }

        private SerializedObject _serializedObject;
        private SerializedProperty _event;

        protected void OnEnable()
        {
            _serializedObject = new SerializedObject(target);
            _event = _serializedObject.FindProperty("_event");
        }

        public override void OnPaintInspectorGUI()
        {
            serializedObject.Update();
            _serializedObject.UpdateIfRequiredOrScript();

            EditorGUILayout.PropertyField(_event, true);

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
            var outputScriptPath = Directory.GetFiles(Directory.GetCurrentDirectory(), "EventBase.cs", SearchOption.AllDirectories).FirstOrDefault();
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
            var gEnum = new Util.DynamicEnum("MapEvents");
            if (gEnum.Save(names))
            {
                AssetDatabase.Refresh();
            }
        }
    }
#endif
}