using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveContoller : MonoBehaviour {

    //ターゲットの座標
<<<<<<< 725cd6c2ec4d0a978c8250c8f58f8bbd7dea784a
    [SerializeField]
=======
>>>>>>> 軌跡の作成
    private Vector3 targetPos = new Vector3(0, 0, 0);

    //スピード
    [SerializeField]
    private float _speed = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        MoveWave();
	}

    //ターゲットの座標まで進む関数
    void MoveWave()
    {
        //計算
        Vector3 pos = targetPos - transform.position;
        //正規化
<<<<<<< 725cd6c2ec4d0a978c8250c8f58f8bbd7dea784a
        transform.position += pos.normalized * _speed;
=======
        transform.position += pos.normalized * 0.4f;
>>>>>>> 軌跡の作成

    }

    //pPos プレイヤーの座標　tPos ターゲットの座標
    void setVelocity(Vector3 tPos)
    {
        targetPos = tPos;
    }

}
