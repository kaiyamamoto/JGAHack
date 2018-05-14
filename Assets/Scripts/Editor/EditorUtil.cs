#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

public class EditorUtil
{
    /// <summary>
    /// ゲームオブジェクトを選択
    /// </summary>
    /// <param name="go"></param>
    public static void Select(GameObject go)
    {
#if UNITY_EDITOR
        Selection.activeGameObject = go;
#endif
    }

    /// <summary>
    /// 現在選択しているオブジェクトの取得
    /// </summary>
    /// <returns></returns>
    public static GameObject GetSelection()
    {
#if UNITY_EDITOR
        return Selection.activeGameObject;
#else
		return null;
#endif
    }

    /// <summary>
    /// ダーティフラグを立てる
    /// </summary>
    /// <param name="obj"></param>
    public static void SetDirty(Object obj)
    {
#if UNITY_EDITOR
        EditorUtility.SetDirty(obj);
#endif
    }

    /// <summary>
    /// ゲームオブジェクトの作成
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="position"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public static GameObject Instantiate(GameObject prefab, Vector3 position, Transform parent)
    {
#if UNITY_EDITOR
        GameObject go = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        go.transform.position = position;
        go.transform.SetParent(parent);
        Undo.RegisterCreatedObjectUndo(go, "Create");
        return go;
#else
		return Instantiate(prefab, position, parent);
#endif
    }

    /// <summary>
    /// オブジェクト破棄
    /// </summary>
    /// <param name="gameObject"></param>
    public static void Destroy(GameObject gameObject)
    {
#if UNITY_EDITOR
        Undo.DestroyObjectImmediate(gameObject);
#else
		Destroy(gameObject);
#endif
    }

    /// <summary>
    /// プレハブのルートを取得
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public static Transform GetPrefabRoot(Transform t)
    {
#if UNITY_EDITOR
        return PrefabUtility.FindPrefabRoot(t.gameObject).transform;
#else
			return t;
#endif
    }
}
