using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Play
{
    public class SwitchKeyEvent : MapEvent.EventBase
    {
        enum Type
        {
            Enter,
            Stay,
        }

        /// <summary>
        /// タイプ
        /// </summary>
        [SerializeField]
        private Type _type = Type.Enter;

        /// <summary>
        /// イベント
        /// </summary>
        public Play.SwitchEvent _event = null;

        protected override void ColliderSetting()
        {
            switch (_type)
            {
                case Type.Enter:
                    SettingEnter();
                    break;
                case Type.Stay:
                    SettingStay();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// ぶつかったとき
        /// </summary>
        /// <param name="action"></param>
        private void SettingEnter()
        {
            _onEnter += (Collider2D other) =>
            {
                if (_event)
                {
                    _event.KeyTriggered();
                }
            };
        }

        /// <summary>
        /// ぶつかっている時
        /// </summary>
        /// <param name="action"></param>
        private void SettingStay()
        {
            _onStay += (Collider2D other) =>
            {
                if (_event)
                {
                    _event.KeyStayed();
                }
            };

            _onExit += (Collider2D other) =>
            {
                if (_event)
                {
                    _event.KeyCanceled();
                }
            };
        }
    }
}