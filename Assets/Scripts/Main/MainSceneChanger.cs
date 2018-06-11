using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;
using DG.Tweening;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Main
{
    public class MainSceneChanger : MonoBehaviour
    {
        // 遷移する際押すマウスの対応した番号
        [SerializeField]
        private int _button = 0;

        // 遷移するシーンの名前
        [SerializeField]
        private string _sceneName = string.Empty;

        [SerializeField]
        private GameObject _backStage = null;

        private void Awake()
        {
            // Display 取得
            var name = TakeOverData.Instance.DisplayName;
            var obj = transform.Find(name);
            var display = obj.GetComponent<Util.Display.DisplayBase>();

            // 初期ディスプレイ
            Util.Display.DisplayManager.Instance.ChangeDisplay(display);

            // ステージタイムデータ読み込み
            StageTimeData.Instance.Initialize();
        }

        void Start()
        {
            StartCoroutine(StageLoad());
        }

        private IEnumerator StageLoad()
        {
            // アセットのロード
            var stageAsset = Resources.LoadAsync("Stage/TestGrid");

            // ロード待ち
            yield return new WaitWhile(() => !stageAsset.isDone);

            var stageObj = stageAsset.asset as GameObject;
            var stage = Instantiate(stageObj);
            _backStage.transform.SetChild(stage);

            var pos = stage.transform.localPosition;
            pos.x = -7.0f;
            stage.transform.localPosition = pos;
            stage.transform.DOLocalMoveX(pos.x + 10.0f, 5.0f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        }
    }
}