using System;
using UnityEngine;

namespace Play.MapEvent
{
    public abstract class EventBase : MonoBehaviour
    {
        // イベント共通で設定するものがある場合ここ
        protected Action<Collider2D> _onEnter = null;
        protected Action<Collider2D> _onStay = null;
        protected Action<Collider2D> _onExit = null;

        protected abstract void ColliderSetting();

        private void Start()
        {
            ColliderSetting();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_onEnter != null)
            {
                _onEnter(other);
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (_onStay != null)
            {
                _onStay(other);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (_onExit != null)
            {
                _onExit(other);
            }
        }
    }
}