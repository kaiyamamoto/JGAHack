using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Play.MapEvent
{
    public class GoalEvent : EventBase
    {
        protected override void ColliderSetting()
        {
            _onEnter += (Collider2D other) =>
            {
                InGameManager.Instance.StageClear();
            };
        }
    }
}