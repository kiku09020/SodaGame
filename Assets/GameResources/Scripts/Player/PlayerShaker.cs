using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player {
	/// <summary> �U��@�\ </summary>
	public class PlayerShaker : PlayerComponent {
		/* Fields */

		[Header("Components")]
		[SerializeField] PlayerSodaManager sodaManager;

		[Header("Parameters")]
		[SerializeField, Tooltip("�U���Ă邩�ǂ�����臒l")] float shakableThreshold = 5;

		/// <summary> �V�F�C�N���� </summary>
		public bool IsShaking { get; private set; }

		float shakePower;       // �ǂ񂾂��U������

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */

		protected override void OnFixedUpdate()
		{
			Shake();
		}

		//-------------------------------------------------------------------
		/* Methods */
		void Shake()
		{
			// �{�^���������Ă����ԂŁA�����x�̑傫����臒l�ȏ�̒l�ȏゾ������A�U���Ă��锻��ɂ���
			IsShaking = PlayerController.ActiveController.IsPressed &&
						DeviceDataReceiver.Acc.magnitude > shakableThreshold;

			if (IsShaking) {
				// �f�o�C�X�̉����x�����Z
				shakePower += DeviceDataReceiver.Acc.magnitude;
			}

			else if(!PlayerController.ActiveController.IsPressed) {
				shakePower = 0;
			}

			if (isDebug) {
				print(shakePower);
			}
		}
	}
}