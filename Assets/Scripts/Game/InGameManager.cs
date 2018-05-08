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
            End
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
        private CameraManager _cameraManager = null;

        /// <summary>
        /// ゲームの開始
        /// </summary>
        public void GameStart()
        {
            _state = State.Play;
        }

        /// <summary>
        /// ゲームの終わり
        /// </summary>
        public void GameEnd()
        {
            _state = State.End;
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