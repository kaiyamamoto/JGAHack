using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Play
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private float _moveSpeed;

        private Rigidbody2D _rigidbody;

        public enum State
        {
            Alive,
            Dead
        }

        [SerializeField]
        private State playerState = State.Alive;

        private bool _terrain = true;

        void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            if (playerState == State.Dead)
            {
                _rigidbody.velocity = Vector2.zero;
                return;
            }

            var controller = GameController.Instance;

            // 移動処理とポーズ処理
            Vector3 tryMove = Vector3.zero;
            if (controller.GetConnectFlag())
            {
                tryMove = ControllerControl(controller);
                if (controller.ButtonDown(Button.START))
                {
                    // ポーズ切り替え
                    ChangePause();
                }
            }
            else
            {
                tryMove = KeyboardControl();
                if (Input.GetKeyDown(KeyCode.P))
                {
                    // ポーズ切り替え
                    ChangePause();
                }
            }

            gameObject.GetComponent<PlayerAnimController>().ChangeAnim(tryMove);
            _rigidbody.velocity = Vector3.ClampMagnitude(tryMove, 1f) * _moveSpeed;
        }

        /// <summary>
        /// キーボードでの操作
        /// </summary>
        private Vector3 KeyboardControl()
        {
            Vector3 tryMove = Vector3.zero;

            if (Input.GetKey(KeyCode.LeftArrow))
                tryMove += Vector3Int.left;
            if (Input.GetKey(KeyCode.RightArrow))
                tryMove += Vector3Int.right;
            if (Input.GetKey(KeyCode.UpArrow))
                tryMove += Vector3Int.up;
            if (Input.GetKey(KeyCode.DownArrow))
                tryMove += Vector3Int.down;

            return tryMove;
        }

        /// <summary>
        /// コントローラーの操作
        /// </summary>
        private Vector3 ControllerControl(GameController con)
        {
            Vector3 tryMove = Vector3.zero;

            if (con.Move(Direction.Left))
                tryMove += Vector3Int.left;
            if (con.Move(Direction.Right))
                tryMove += Vector3Int.right;
            if (con.Move(Direction.Front))
                tryMove += Vector3Int.up;
            if (con.Move(Direction.Back))
                tryMove += Vector3Int.down;

            return tryMove;
        }

        /// <summary>
        /// ゲームポーズの切り替え
        /// </summary>
        private void ChangePause()
        {
            var instance = InGameManager.Instance;
            var stats = instance.GameState;
            if (stats == InGameManager.State.Pause)
            {
                instance.GamePause(false);
            }
            else
            {
                instance.GamePause(true);
            }
        }
    }
}