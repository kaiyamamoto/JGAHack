﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PopUp : MonoBehaviour
{

	enum Select
	{
		Yes = 0,
		No = 1,
		length = 2
	}

	[SerializeField]
	private GameObject _obj = null;

	[SerializeField]
	private Text _text = null;

	[SerializeField]
	private Image _arrow = null;

	[SerializeField]
	private Text _yes = null;

	[SerializeField]
	private Text _no = null;

	private Text[] _select = new Text[(int)Select.length];

	[SerializeField, Extensions.ReadOnly]
	private int _selectNum = 0;

	private Tween _move = null;

	private bool _push = false;

	public IEnumerator ShowPopUp(string text, System.Action<bool> action)
	{
		_text.text = text;

		_select[0] = _yes;
		_select[1] = _no;

		// 矢印の設定
		SetArrow();

		Time.timeScale = 0.0f;

		yield return new WaitWhile(() => !_push);

		Time.timeScale = 1.0f;

		bool flag = false;
		if (_selectNum == 0) flag = true;

		action(flag);

		Destroy(this.gameObject);

	}

	void Update()
	{
		KeyInput();
	}

	void KeyInput()
	{
		if ((Input.GetKeyDown(KeyCode.RightArrow)) ||
			(Input.GetKeyDown(KeyCode.LeftArrow)))
		{
			_selectNum++;
			if ((int)Select.length <= _selectNum) _selectNum = 0;

			SetArrow();
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			_push = true;
		}
	}

	private void SetArrow()
	{
		var pos = _arrow.transform.localPosition;
		var arrowPos = pos;
		arrowPos.x = GetleftSidePos(_select[_selectNum]);
		_arrow.transform.localPosition = arrowPos;

		_move.Kill();
		_move = _arrow.transform.DOLocalMoveX(arrowPos.x + 20.0f, 0.2f).SetLoops(-1, LoopType.Yoyo).SetUpdate(true);
	}

	private float GetleftSidePos(Text textObj)
	{
		var size = textObj.rectTransform.sizeDelta;
		var pos = textObj.transform.localPosition;

		return pos.x - size.x;
	}

}
