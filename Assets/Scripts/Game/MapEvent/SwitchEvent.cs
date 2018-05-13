using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Play.MapEvent
{
	public class SwitchEvent : EventBase
	{
		enum Type
		{
			Enter,
			Stay,
		}

		/// <summary>
		/// スイッチ
		/// </summary>
		[SerializeField]
		private bool _switch = false;
		public bool Switch { get { return _switch; } }

		/// <summary>
		/// タイプ
		/// </summary>
		[SerializeField]
		private Type _type = Type.Enter;

		protected override void ColliderSetting()
		{
			System.Action<Collider2D, bool> action = (Collider2D other, bool flag) =>
			 {
				 // プレイヤーが当たったらON
				 if (other.GetComponent<Player>())
				 {
					 _switch = flag;
				 }
			 };

			switch (_type)
			{
				case Type.Enter:
					SettingEnter(action);
					break;
				case Type.Stay:
					SettingStay(action);
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// ぶつかったとき
		/// </summary>
		/// <param name="action"></param>
		private void SettingEnter(System.Action<Collider2D, bool> action)
		{
			_onEnter += (Collider2D other) =>
			{
				action(other, true);
			};
		}

		/// <summary>
		/// ぶつかっている時
		/// </summary>
		/// <param name="action"></param>
		private void SettingStay(System.Action<Collider2D, bool> action)
		{
			_onStay += (Collider2D other) =>
			{
				action(other, true);
			};

			_onExit += (Collider2D other) =>
			{
				action(other, false);
			};
		}
	}
}