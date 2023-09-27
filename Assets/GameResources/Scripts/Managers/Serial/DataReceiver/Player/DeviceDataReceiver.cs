using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceDataReceiver : DataReceiver_Base {

	[SerializeField] bool isDebug;

	/// <summary> �f�o�C�X�̌X�� </summary>
	public static Vector3 Acc { get; private set; }

	/// <summary> �{�^���������ꂽ���ǂ��� </summary>
	public static bool IsPressedA { get; private set; }

	public static bool IsPressedB { get; private set; }

	protected override void OnReceivedData()
	{
		var data = handler.GetSplitedData();     // �f�[�^�擾

		try {
			// �f�[�^�����񂩂�float�^�ɕϊ����āA�x�N�g���ɓK�p
			float x = float.Parse(data[0]);
			float y = float.Parse(data[1]);
			float z = float.Parse(data[2]);

			Acc = new Vector3(x, y, z);

			// �����ꂽ���ǂ�����bool�^�ɕϊ�
			IsPressedA = int.Parse(data[3]) != 0;
			IsPressedB = int.Parse(data[4]) != 0;

			if (isDebug && Debug.isDebugBuild) {
				print($"{Acc},{IsPressedA},{IsPressedB}");
			}
		}

		// ��O
		catch (System.Exception exc) {
			Debug.LogWarning(exc.Message);
		}
	}
}
