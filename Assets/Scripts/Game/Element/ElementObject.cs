using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Play.Element
{
	// 要素を持つオブジェクトクラス
	public class ElementObject : MonoBehaviour
	{
		[SerializeField, Extensions.ReadOnly]
		private List<ElementBase> _elementList = null;

		private void Start()
		{
			_elementList = new List<ElementBase>();
			var array = this.GetComponents<ElementBase>();
			_elementList.AddRange(array);

			foreach (var element in _elementList)
			{
				element.Initialize();
			}
		}
	}
}