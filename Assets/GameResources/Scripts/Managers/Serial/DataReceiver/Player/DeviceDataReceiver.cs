using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceDataReceiver : DataReceiver_Base {

	[SerializeField] bool isDebug;

	/// <summary> デバイスの傾き </summary>
	public static Vector3 Acc { get; private set; }

	/// <summary> ボタンが押されたかどうか </summary>
	public static bool IsPressedA { get; private set; }

	public static bool IsPressedB { get; private set; }

	protected override void OnReceivedData()
	{
		var data = handler.GetSplitedData();     // データ取得

		try {
			// データ文字列からfloat型に変換して、ベクトルに適用
			float x = float.Parse(data[0]);
			float y = float.Parse(data[1]);
			float z = float.Parse(data[2]);

			Acc = new Vector3(x, y, z);

			// 押されたかどうかをbool型に変換
			IsPressedA = int.Parse(data[3]) != 0;
			IsPressedB = int.Parse(data[4]) != 0;

			if (isDebug && Debug.isDebugBuild) {
				print($"{Acc},{IsPressedA},{IsPressedB}");
			}
		}

		// 例外
		catch (System.Exception exc) {
			Debug.LogWarning(exc.Message);
		}
	}
}
