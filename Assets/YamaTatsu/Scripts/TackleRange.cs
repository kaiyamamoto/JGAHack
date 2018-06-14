using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TackleRange : MonoBehaviour {

    private float _time = 0.0f;

    private float _intervalTime = 1.0f;

    private bool _flag = true;

    [SerializeField]
    private float _cnt;

    GameObject Tackle;
    SpriteRenderer spriteRenderer;

   
    // Use this for initialization
    void Start () {

        Tackle = transform.Find("TackleRange").gameObject;

        spriteRenderer = transform.Find("TackleRange").GetComponent<SpriteRenderer>();

        spriteRenderer.bounds.size.Scale.

    }
	
	// Update is called once per frame
	void Update () {

        _time += Time.deltaTime;

        if(_time > 1.0f)
        {
            _intervalTime += Time.deltaTime;
            if(_intervalTime >=_cnt)
            {
                _intervalTime = 0.0f;
                _flag = !_flag;

                if(_flag)
                {
                    Tackle.SetActive(true);
                }
                else
                {
                    Tackle.SetActive(false);
                }

            }
        }
        
    }
    
}
