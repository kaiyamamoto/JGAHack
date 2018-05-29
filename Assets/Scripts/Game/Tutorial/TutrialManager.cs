using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;
using Extensions;

namespace Play.Turrial
{
    public class TutrialManager : SingletonMonoBehaviour<TutrialManager>
    {
        [SerializeField]
        private string _initText = string.Empty;

        void Start()
        {
            StartCoroutine(StartText());
        }

        IEnumerator StartText()
        {
            var manager = InGameManager.Instance;
            var cameraMan = manager.CameraManager;

            yield return new WaitUntil(() => cameraMan.GetEndProduction());

            var messenger = manager.Messenger;
            messenger.SetMessagePanel(_initText);
        }
    }
}