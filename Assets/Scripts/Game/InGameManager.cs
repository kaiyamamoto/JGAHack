using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Play
{
    // インゲームの管理クラス
    public class InGameManager : Util.SingletonMonoBehaviour<InGameManager>
    {
        // ゲームの状態
        public enum State
        {
            Stand = 0,
            Play,
            Pause,
            Clear,
            Over
        }

        // 状態
        [SerializeField, Extensions.ReadOnly]
        private State _state = State.Stand;
        public State GameState
        {
            get { return _state; }
        }

        // UIRoot
        [SerializeField]
        private GameObject _uiRoot = null;
        public GameObject UIRoot
        {
            get { return _uiRoot; }
        }

        // ElementRoot
        [SerializeField]
        private GameObject _elementObjRoot = null;
        public GameObject ElementObjRoot
        {
            get { return _elementObjRoot; }
        }

        // カメラ
        [SerializeField]
        private Stage.StageManager _stageManager = null;

        /// <summary>
        /// ゲームの開始
        /// </summary>
        public void GameStart()
        {
            _state = State.Play;
        }

        /// <summary>
        /// ステージのクリア
        /// </summary>
        public void StageClear()
        {
            _state = State.Clear;
        }

        /// <summary>
        /// ステージの失敗
        /// </summary>
        public void StageOver()
        {
            _stageManager.ReTry();
        }

        /// <summary>
        /// ゲームの一時停止
        /// </summary>
        public void GamePause()
        {
            _state = State.Pause;
        }
    }
}