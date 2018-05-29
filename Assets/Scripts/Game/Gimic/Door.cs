
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Play
{
    public class Door : SwitchEvent
    {
        // 対応しているキー
        public Doorkey _key;

        private SpriteRenderer _blocker;
        private Collider2D _collider;

        void Start()
        {
            _blocker = this.GetComponent<SpriteRenderer>();
            _collider = this.GetComponent<BoxCollider2D>();
        }

        public override void KeyStayed()
        {
            _blocker.enabled = false;
            _collider.enabled = false;
        }

        public override void KeyCanceled()
        {
            _blocker.enabled = true;
            _collider.enabled = true;
        }
    }
}