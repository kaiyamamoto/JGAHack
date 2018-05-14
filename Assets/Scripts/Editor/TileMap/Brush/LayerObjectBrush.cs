using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public abstract class LayerObjectBrush<T> : GridBrushBase
{
    public T ActiveObject { get { return EditorUtil.GetSelection() != null ? EditorUtil.GetSelection().GetComponent<T>() : default(T); } }
    public T[] AllObjects { get { return BrushUtil.GetRootGrid(false) != null ? BrushUtil.GetRootGrid(false).GetComponentsInChildren<T>() : default(T[]); } }

    protected virtual Vector3 OffsetFromBottomLeft { get { return _prefabOffset; } }
    public virtual bool AlwaysCreateOnPaint { get { return false; } }
    public GameObject _prefab;
    public string _layerName;
    public Vector3 _prefabOffset;

    public override void Paint(GridLayout grid, GameObject layer, Vector3Int position)
    {
        if (_prefab == null)
        {
            Debug.LogError("プレハブがnullなので操作がキャンセルされました.");
            return;
        }

        if (string.IsNullOrEmpty(_layerName))
        {
            Debug.LogError("レイヤー名が空です。操作がキャンセルされました.");
            return;
        }

        if (ActiveObject == null || AlwaysCreateOnPaint)
        {
            // activeなオブジェクトがない場合
            T obj = GetObject(grid, position);
            if (obj is Component)
            {
                EditorUtil.Select((obj as Component).gameObject);
            }
            else
            {
                CreateObject(grid, position, _prefab);
            }
        }
    }

    /// <summary>
    /// オブジェクトを作成
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="position"></param>
    /// <param name="prefab"></param>
    protected void CreateObject(GridLayout grid, Vector3Int position, GameObject prefab)
    {
        if (_prefab.GetComponent<T>() != null)
        {
            GameObject newObj = EditorUtil.Instantiate(prefab, grid.LocalToWorld(grid.CellToLocalInterpolated(position + OffsetFromBottomLeft)), GetLayer());
            EditorUtil.Select(newObj);
        }
        else
        {
            Debug.LogError("Prefab " + _prefab.name + " が存在しないので " + typeof(T) + ", 操作がキャンセルされました.");
        }
    }

    /// <summary>
    /// けしけし
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="layer"></param>
    /// <param name="position"></param>
    public override void Erase(GridLayout grid, GameObject layer, Vector3Int position)
    {
        T obj = GetObject(grid, position);
        if (obj is Component)
        {
            // 消す
            EditorUtil.Destroy((obj as Component).gameObject);
            EditorUtil.Select(GetLayer().gameObject);
        }
    }

    /// <summary>
    /// 選択
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="layer"></param>
    /// <param name="position"></param>
    /// <param name="pivot"></param>
    public override void Pick(GridLayout grid, GameObject layer, BoundsInt position, Vector3Int pivot)
    {
        T obj = GetObject(grid, position.min);
        if (obj is Component)
        {
            EditorUtil.Select((obj as Component).gameObject);
        }
        else
        {
            EditorUtil.Select(GetLayer().gameObject);
        }
    }

    /// <summary>
    /// オブジェクトの取得
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    public T GetObject(GridLayout grid, Vector3Int position)
    {
        Transform parent = GetLayer();
        List<GameObject> children = new List<GameObject>();
        for (int i = 0; i < parent.childCount; i++)
        {
            Vector3 p = parent.GetChild(i).position;
            if (grid.WorldToCell(p) == position)
                children.Add(parent.GetChild(i).gameObject);
        }
        return GetObject(children);
    }

    public T GetObject(List<GameObject> gameObjects)
    {
        foreach (var gameObject in gameObjects)
        {
            T obj = gameObject.GetComponent<T>();
            if (obj != null)
            {
                return obj;
            }
        }
        return default(T);
    }

    /// <summary>
    /// レイヤーの取得
    /// </summary>
    /// <returns></returns>
    public Transform GetLayer()
    {
        Transform layer = BrushUtil.GetRootGrid(false).transform.Find(_layerName);
        if (layer == null)
        {
            GameObject newGameObject = new GameObject(_layerName);
#if UNITY_EDITOR
            Undo.RegisterCreatedObjectUndo(newGameObject, "Create " + _layerName);
#endif
            layer = newGameObject.transform;
            layer.SetParent(BrushUtil.GetRootGrid(false).transform);
        }
        return layer;
    }

    public Vector3Int WorldToLocal(Grid grid, Vector3Int worldPosition)
    {
        return ActiveObject is Component ? worldPosition - grid.WorldToCell((ActiveObject as Component).transform.position) : default(Vector3Int);
    }

}
