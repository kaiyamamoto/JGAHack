using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Play.Element;

namespace Play.Tutrial
{
	public class TutrialSelector : ElementSelector
	{
		[SerializeField]
		private ElementObject _step2Object = null;
		[SerializeField]
		private ElementObject _step4Object = null;

		[SerializeField]
		private string _step3Text = string.Empty;
		[SerializeField]
		private string _step4Text = string.Empty;
		[SerializeField]
		private string _step5Text = string.Empty;

		/// <summary>
		/// オブジェクトをターゲット
		/// </summary>
		override protected void TargetObject(ElementObject obj)
		{
			var manager = InGameManager.Instance;
			var messenger = manager.Messenger;

			var tutrial = TutrialManager.Instance;
			if (tutrial.Step == 2)
			{
				// ステップ2のとき
				if (LockOnObj.CheckOnScreen(_step2Object.transform.position))
				{
					base.TargetObject(_step2Object);
					// 3に移行
					tutrial.StepSet(3);
					messenger.SetMessagePanel(_step3Text);
				}
			}

			if (tutrial.Step == 4)
			{
				// ステップ4のとき
				if (LockOnObj.CheckOnScreen(_step2Object.transform.position))
				{
					base.TargetObject(_step4Object);
					// 5に移行
					tutrial.StepSet(5);
					messenger.SetMessagePanel(_step5Text);
				}
			}
		}

		/// <summary>
		/// オブジェクトを選択
		/// </summary>
		override protected void SelectObject()
		{
			var manager = InGameManager.Instance;
			var messenger = manager.Messenger;

			var tutrial = TutrialManager.Instance;

			if (tutrial.Step == 3)
			{
				// ステップ3のとき
				if (_targetObject)
				{
					base.SelectObject();
					// 4に移行
					tutrial.StepSet(4);
					messenger.SetMessagePanel(_step4Text);
				}
			}
		}

		/// <summary>
		/// 要素の移動
		/// </summary>
		/// <param name="selectObj"></param>
		override protected void MoveElement(ElementObject selectObj)
		{
			base.MoveElement(selectObj);
		}

	}
}