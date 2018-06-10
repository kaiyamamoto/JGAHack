using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Util.Save;

namespace Play.Stage
{
	public class StageTimeData : SaveSingleton<StageTimeData>
	{
		[SerializeField]
		private Serialization<string, float> _stageTime;

		public Serialization<string, float> StageTime
		{
			get { return _stageTime; }
		}

		override public void Save(string json = "")
		{
			json = JsonUtility.ToJson(_stageTime);
			base.Save(json);
		}

		/// <summary>
		/// 初期化
		/// </summary>
		public void Initialize()
		{
			// セーブデータの読み込み
			if (Load() == false)
			{
				_stageTime = new Serialization<string, float>(new Dictionary<string, float>());
				for (int i = 0; i < 20; i++)
				{
					var dic = _stageTime.ToDictionary();
					for (int j = 0; j < 3; j++)
					{
						string key = i.ToString() + "_" + j.ToString();
						dic.Add(key, 0.0f);
					}
				}
			}
		}
	}
}