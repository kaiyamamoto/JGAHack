using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

			Vector3 tryMove = Vector3.zero;

			if (Input.GetKey(KeyCode.LeftArrow))
				tryMove += Vector3Int.left;
			if (Input.GetKey(KeyCode.RightArrow))
				tryMove += Vector3Int.right;
			if (Input.GetKey(KeyCode.UpArrow))
				tryMove += Vector3Int.up;
			if (Input.GetKey(KeyCode.DownArrow))
				tryMove += Vector3Int.down;

			_rigidbody.velocity = Vector3.ClampMagnitude(tryMove, 1f) * _moveSpeed;
		}
	}
}