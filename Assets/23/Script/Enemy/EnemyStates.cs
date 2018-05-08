using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Play.Enemy
{
    //敵用ステータス
    public class EnemyStates : MonoBehaviour
    {

        //体力
        [SerializeField]
        int HP = 3;

        
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }



        void OnTriggerEnter2D(Collider2D col)
        {    

           
                //被ダメ
                HP--;
               
                //体力が0になったら消滅
                if (HP <= 0)
                {
                    Destroy(this.gameObject);
                }
        
        }
    }
}