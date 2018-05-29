using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;
using Extensions;

namespace Play.Turrial
{
    public class TutrialManager : SingletonMonoBehaviour<TutrialManager>
    {
        void Start()
        {
            var inManager = InGameManager.Instance;

            // テキストパネルを表示
            var messenger = inManager.Messenger;
            messenger.ShowWindow();

            StartCoroutine(test());

        }

        IEnumerator test()
        {
            var inManager = InGameManager.Instance;
            var messenger = inManager.Messenger;

            messenger.SetMessagePanel("「やったぜ」");
            yield return new WaitForSeconds(5.0f);
            messenger.SetMessagePanel("「投稿者」");
            yield return new WaitForSeconds(5.0f);
            messenger.SetMessagePanel("「変態糞土方」");
            yield return new WaitForSeconds(5.0f);
        }

    }
}