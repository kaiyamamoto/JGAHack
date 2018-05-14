using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Play
{
    public abstract class SwitchEvent : MonoBehaviour
    {
        /// <summary>
        /// スイッチキー
        /// </summary>
        [SerializeField, Extensions.ReadOnly]
        private bool _switchKey = false;
        public bool Key { get { return _switchKey; } }

        /// <summary>
        /// スイッチがオンになった時
        /// </summary>
        public virtual void KeyTriggered()
        {

        }

        public virtual void KeyStayed()
        {

        }

        /// <summary>
        /// スイッチが解除された時
        /// </summary>
        public virtual void KeyCanceled()
        {

        }
    }
}