using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Play
{
	public class Timer : MonoBehaviour
	{
		// 分数カウント
		private int _minuteCount;
		// 秒数カウント
		private float _secondCount;
		// 経過時間カウント（ミリ秒）
		private float _msecondCount;
		// 計測中か
		private bool _isCounting = false;

		//時間計測開始
		public void StartTimer()
		{
			if (_isCounting) return;

			_isCounting = true;
			ResetTimer();

			StartCoroutine(TimerCorutine());
		}
		private IEnumerator TimerCorutine()
		{
			//計測中なら
			while (_isCounting)
			{
				//時間加算
				_secondCount += Time.deltaTime;
				_msecondCount += Time.deltaTime;

				if (_secondCount >= 60.0f)
				{
					_minuteCount++;
					_secondCount -= 60.0f;
				}
				yield return null;
			}
		}

		//時間計測停止
		public void StopTimer()
		{
			_isCounting = false;
		}

		//時間計測リセット
		public void ResetTimer()
		{
			_minuteCount = 0;
			_secondCount = 0;
			_msecondCount = 0;
		}

		// テキストに表示
		public string DisplayTime()
		{
			return _minuteCount.ToString("00") + ":" + _secondCount.ToString("00.00");
		}

		// タイム取得（分）
		public int GetMinute()
		{
			return _minuteCount;
		}

		// タイム取得（秒）
		public float GetSecond()
		{
			return _secondCount;
		}

		// タイム取得（ミリ秒）
		public float GetMillisecond()
		{
			return _msecondCount;
		}
	}
}