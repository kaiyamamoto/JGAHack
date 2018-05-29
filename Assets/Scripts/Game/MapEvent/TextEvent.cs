using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Play.MapEvent
{
    public class TextEvent : EventBase
    {
        [SerializeField]
        private string _text = string.Empty;

        [SerializeField]
        private bool isDead = false;

        protected override void ColliderSetting()
        {
            _onEnter += (Collider2D other) =>
            {
                if (other.GetComponent<Player>())
                {
                    var messenger = InGameManager.Instance.Messenger;
                    messenger.SetMessagePanel(_text);
                    if (isDead) Destroy(this);
                }
            };
        }
    }
}