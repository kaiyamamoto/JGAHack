using UnityEngine;
using Play;

namespace UnityEditor
{
    [CreateAssetMenu]
    [CustomGridBrush(false, true, false, "Door")]
    public class DoorBrush : LayerObjectBrush<Door>
    {
        public GameObject _keyPrefab;

        /// <summary>
        /// ペイント
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="layer"></param>
        /// <param name="position"></param>
        public override void Paint(GridLayout grid, GameObject layer, Vector3Int position)
        {
            if (ActiveObject != null)
            {
                if (ActiveObject._key == null)
                {
                    // キーを作成して設定
                    var newKey = EditorUtil.Instantiate(_keyPrefab, grid.LocalToWorld(grid.CellToLocalInterpolated(position + OffsetFromBottomLeft)), GetLayer());
                    var key = newKey.GetComponent<Doorkey>();
                    key._event = ActiveObject;
                    EditorUtil.SetDirty(newKey);

                    ActiveObject._key = key;
                    EditorUtil.SetDirty(ActiveObject);
                }
                else
                {
                    EditorUtil.Select(BrushUtil.GetRootGrid(false).gameObject);
                }
            }
            base.Paint(grid, layer, position);
        }

        /// <summary>
        /// けしけし
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="layer"></param>
        /// <param name="position"></param>
        public override void Erase(GridLayout grid, GameObject layer, Vector3Int position)
        {
            foreach (var door in AllObjects)
            {
                if (grid.WorldToCell(door.transform.position) == position)
                {
                    DestroyDoor(door);
                    EditorUtil.Select(BrushUtil.GetRootGrid(false).gameObject);
                    return;
                }
                if (door._key != null && grid.WorldToCell(door._key.transform.position) == position)
                {
                    DestroyImmediate(door._key.gameObject);
                    door._key = null;
                    EditorUtil.SetDirty(door);
                }
            }
        }

        /// <summary>
        /// ドアの死
        /// </summary>
        /// <param name="door"></param>
        private void DestroyDoor(Door door)
        {
            if (door._key != null)
                DestroyImmediate(door._key.gameObject);
            DestroyImmediate(door.gameObject);
        }
    }
}