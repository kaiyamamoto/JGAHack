using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;

namespace Play
{
    public class FallObject : MonoBehaviour
    {
        private BoxCollider2D _collider = null;

        public Collider2D Collider
        {
            get { return _collider ? _collider : _collider = GetComponent<BoxCollider2D>(); }
        }

        private Transform _parent = null;

        private bool _check = false;
        private bool _fall = false;
        private RideFloor _ride = null;

        // 親の前フレームの位置
        private Vector3 _old;

        void Awake()
        {
            _parent = transform.parent;
            SetParent(_parent);
        }

        void FixedUpdate()
        {
            var v = transform.parent.transform.position - _old;
            transform.position = transform.position + v * Time.deltaTime;
            _old = transform.parent.transform.position;

            if (!_check) return;

            if (_ride)
            {
                // 乗る(親子関係)
                if (_ride.transform != transform.parent)
                {
                    SetParent(_ride.transform.parent.transform);
                }
            }
            else
            {
                if (_parent)
                {
                    SetParent(_parent);
                }
            }

            if (_fall && (!_ride))
            {
                Debug.Log("落ちたな");
                Play.InGameManager.Instance.StageOver();
            }

            _fall = false;
            _check = false;
            _ride = null;
        }

        void OnTriggerStay2D(Collider2D other)
        {
            // 足元で判定
            var myBounds = Collider.bounds;
            if (other.bounds.Contains(myBounds.center - new Vector3(0.0f, myBounds.extents.y, 0.0f)))
            {
                if (other.tag == "Hole")
                {
                    _fall = true;
                }

                var ride = other.GetComponent<RideFloor>();
                if (ride)
                {
                    _ride = ride;
                }
                _check = true;
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            // 離れたとき親子関係解除
            var ride = other.GetComponent<RideFloor>();
            if (!ride) return;

            if (ride.transform.parent == transform.parent)
            {
                SetParent(_parent);
            }
        }

        void SetParent(Transform parent)
        {
            transform.parent = parent;
            _old = parent.transform.position;
        }
    }
}