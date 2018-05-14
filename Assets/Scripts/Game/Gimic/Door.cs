
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

        void Start()
        {
            _blocker = this.GetComponent<SpriteRenderer>();
        }

        public override void KeyStayed()
        {
            _blocker.enabled = false;
        }

        public override void KeyCanceled()
        {
            _blocker.enabled = true;
        }
    }
}