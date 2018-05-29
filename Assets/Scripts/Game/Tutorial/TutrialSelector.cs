using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Play.Element;

namespace Play.Turrial
{
    public class TutrialSelector : ElementSelector
    {
        /// <summary>
        /// オブジェクトをターゲット
        /// </summary>
        override protected void TargetObject(ElementObject obj)
        {
            base.TargetObject(obj);
        }

        /// <summary>
        /// オブジェクトを選択
        /// </summary>
        override protected void SelectObject()
        {
            base.SelectObject();
        }

        /// <summary>
        /// 要素の移動
        /// </summary>
        /// <param name="selectObj"></param>
        override protected void MoveElement(ElementObject selectObj)
        {
            base.MoveElement(selectObj);
        }

    }
}