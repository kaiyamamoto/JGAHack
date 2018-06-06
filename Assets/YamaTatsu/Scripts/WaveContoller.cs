using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class WaveContoller : MonoBehaviour {


    private Transform myTrans;

    public Transform _target;

    private Tweener tweener;

    // 起動時
    void Start()
    {
        myTrans = transform;

        StartCoroutine(start());
    }

    private IEnumerator start()
    {
        // 待つ
        yield return new WaitForSeconds(1);

        Sequence seq = DOTween.Sequence();
        // バウンド
        seq.Append(myTrans.DOLocalMoveY(-0.1f, 1f).SetEase(Ease.OutBounce).OnComplete(callback).SetRelative());
        seq.Join(myTrans.DOLocalMoveX(0.3f, 1f).SetRelative());
    }

    // 更新
    void Update()
    {
        
        // バウンド終了後から監視する
        if (tweener != null)
        {
            // ある程度近づいたら消す
            if (Vector3.Distance(myTrans.position, _target.position) < 0.05f) {
                // 消す
                Destroy(gameObject);
            }
            // 終点を更新
            tweener.ChangeEndValue(_target.position, 0.2f, true);
        }
    }

    // バウンド終了後に呼ばれる
    private void callback()
    {
        tweener = myTrans.DOMove(_target.position, 0.2f);
    }


    //targetのtransformを入れる
    public void setTarget(Transform target)
    {
        _target = target;
    }

    //   //ターゲットの座標
    //   [SerializeField]
    //   private GameObject _target;

    //   //スピード
    //   [SerializeField]
    //   private float _speed = 0;

    //   //プレイヤーの座標
    //   private Transform myTrans;

    //   //
    //   private Tweener tweener;

    //// Use this for initialization
    //void Start () {

    //       myTrans = this.gameObject.transform;

    //       StartCoroutine(start());

    //}

    //   private IEnumerator start()
    //   {
    //       //待つ
    //       yield return new WaitForSeconds(1);

    //       Sequence seq = DOTween.Sequence();
    //   }

    //// Update is called once per frame
    //void Update () {

    //       MoveWave();

    //   }

    //   //ターゲットの座標まで進む関数
    //   void MoveWave()
    //   {
    //       //計算
    //       Vector3 pos = _target.transform.position;

    //       pos.x -= 0.005f;

    //       _target.transform.position = pos;

    //      if(tweener != null)
    //       {
    //           //一定距離まできたらデストロイ
    //           if (Vector3.Distance(myTrans.position, _target.transform.position) < 0.05f)
    //           {
    //               gameObject.SetActive(false);
    //           }
    //           tweener.ChangeEndValue(_target.transform.position, 0.2f, true);
    //       }
    //   }

    //   //pPos プレイヤーの座標　tPos ターゲットの座標
    //   public void setTarget(GameObject target)
    //   {
    //       _target = target;
    //   }

    //   // バウンド終了後に呼ばれる
    //   private void callback()
    //   {
    //       tweener = myTrans.DOMove(_target.transform.position, 0.5f);
    //   }

}
