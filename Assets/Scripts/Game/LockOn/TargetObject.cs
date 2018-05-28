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

        // 要素オブジェクト
        private Element.ElementObject _elementObj = null;

        public ElementObject ElementObj
        {
            get
            { return _elementObj; }
        }

        public ElementSelector Selector
        {
            get { return _selector; }
        }

        /// <summary>
        /// 初期化
        /// </summary>
        void Setting()
        {
            _elementObj = GetComponent<Element.ElementObject>();

            // TODO: 仮で選択したオブジェクトにテキストを付与
            // ======================================================
            // 子に要素追加
            var text = GameObject.Instantiate(Selector._elementText);
            transform.SetChild(text.gameObject);
            // ターゲットマーカー作成
            var obj = Instantiate(Selector._target);
            text.transform.SetChild(obj);
            text.transform.localPosition = Vector3.zero;
            text.gameObject.AddComponent<Canvas>();
            var scaler = text.gameObject.AddComponent<CanvasScaler>();
            scaler.dynamicPixelsPerUnit = 20;
            text.transform.localPosition = new Vector3(0.0f, 0.5f, 0.0f);
            text.transform.localScale = new Vector3(0.3f, 0.3f, 1.0f);
            text.fontSize = 1;
            text.alignment = TextAnchor.MiddleLeft;


            // ターゲットされていることを自覚させる

            // テキスト変更

            text.text = string.Empty;
            foreach (var element in ElementObj.ElementList)
            {
                if (element)
                {
                    text.text += element.Type.ToString() + "\n";
                }
            }
            if (text.text == string.Empty)
            {
                text.text = "NoneElement";
            }
            // ======================================================
        }

        /// <summary>
        /// セレクターの設定
        /// </summary>
        /// <param name="selector"></param>
        public void SetSelector(ElementSelector selector)
        {
            _selector = selector;

            Setting();
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
            var childs = transform.GetAllChild();
            foreach (var c in childs)
            {
                if (c.name == "ElementText(Clone)")
                {
                    Destroy(c.gameObject);
                }
            }
            Destroy(this);

            _selector.TargetRelease();
        }
    }
}