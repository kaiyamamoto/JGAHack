using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Extensions;
using Play.Element;

namespace Play.LockOn
{
    public class TargetObject : MonoBehaviourEx
    {
        // 参照しているセレクター
        private ElementSelector _selector = null;
        public ElementSelector Selector
        {
            get { return _selector; }
        }

        // 要素オブジェクト
        private Element.ElementObject _elementObj = null;

        public ElementObject ElementObj
        {
            get
            { return _elementObj; }
        }

        /// <summary>
        /// 初期化
        /// </summary>
        void InitSetting()
        {
            _elementObj = GetComponent<Element.ElementObject>();
        }

        /// <summary>
        /// セレクターの設定
        /// </summary>
        /// <param name="selector"></param>
        public void SetSelector(ElementSelector selector)
        {
            _selector = selector;

            InitSetting();
        }

        void Update()
        {
            var lockOn = Selector.LockOnObj;
            if (!lockOn.CheckOnScreen(transform.position))
            {
                // 画面外に出たらターゲット解除
                Release();
            }
        }

        public void Release()
        {
            // 選択解除
            Destroy(this);

            _selector.TargetRelease();
        }
    }
}