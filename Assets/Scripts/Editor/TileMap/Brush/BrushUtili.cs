#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using UnityEngine.Tilemaps;

public class BrushUtil
{
    const string GRID_NAME = "SearchGrid";

    /// <summary>
    /// 現在のグリッドを取得
    /// </summary>
    /// <param name="autoCreate"></param>
    /// <returns></returns>
    public static Grid GetRootGrid(bool autoCreate)
    {
        Grid result = null;

        // 現在の選択
        if (EditorUtil.GetSelection() != null)
            result = EditorUtil.GetSelection().GetComponentInParent<Grid>();

        // 選択されていない場合
        if (result == null)
        {
            // Grid で探す
            GameObject gridGameObject = GameObject.Find(GRID_NAME).transform.parent.gameObject;
            if (gridGameObject != null && gridGameObject.GetComponent<Grid>() != null)
            {
                // 探したオブジェクトがグリッドを持っている場合
                result = gridGameObject.GetComponent<Grid>();
            }
            else
            {
                throw new System.Exception("Gridが存在しない、またはGridの親がGridを持っていません。");
            }
        }

        // グリッドを返す
        return result;
    }

    /// <summary>
    /// グリッドインフォメーションを取得
    /// </summary>
    /// <param name="autoCreate"></param>
    /// <returns></returns>
    public static GridInformation GetRootGridInformation(bool autoCreate)
    {
        Grid grid = GetRootGrid(autoCreate);
        GridInformation info = grid.GetComponent<GridInformation>();
        if (info == null)
            info = grid.gameObject.AddComponent<GridInformation>();
        return info;
    }

    /// <summary>
    /// タイルマップを追加
    /// </summary>
    /// <param name="layer"></param>
    /// <returns></returns>
    public static Tilemap AddTilemap(Transform layer)
    {
        Tilemap result = null;
#if UNITY_EDITOR
        result = Undo.AddComponent<Tilemap>(layer.gameObject);
        TilemapRenderer r = Undo.AddComponent<TilemapRenderer>(layer.gameObject);
        r.sortingOrder = 0;
        r.sharedMaterial = AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/tilemap lit.mat");
#else
				result = layer.gameObject.AddComponent<Tilemap>();
				layer.gameObject.AddComponent<TilemapRenderer>();
#endif
        return result;
    }
}

