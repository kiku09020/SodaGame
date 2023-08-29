using Game.Player.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Player {
	/// <summary> �v���C���[�̒Y�_�̊Ǘ� </summary>
	public class PlayerSodaManager : PlayerComponent {
		/* Fields */

		[Header("Parameters")]
		[SerializeField] float maxPower = 200f;
		[SerializeField] float removePowerAmount = .2f;

		float power;		// ���݂̗�

		[Header("UI")]
		[SerializeField] Image filledImage;

		//-------------------------------------------------------------------
		/* Properties */
		float powerRate => power / maxPower;		// �� / �ő��

		/// <summary> �\�[�_�����o���\�� </summary>
		public bool IsSodable { get; private set; }

		//-------------------------------------------------------------------
		/* Events */
		private void FixedUpdate()
		{
			IsSodable = (power > 0) ? true : false;		// ��s�\�t���O�̔���

			filledImage.fillAmount = powerRate;			// FillAmount�̔��f
		}

		// �\�[�_��Ԃ̎���Update
		public void SodaUpdate()
		{
			RemovePower();                              // �p���[���炷

			// �\�[�_��s�s�\�ɂȂ�΁A�ʏ��ԂɑJ�ڂ���
			if (!IsSodable) {
				stateMachine.StateTransition("Normal");
			}
		}

		//-------------------------------------------------------------------
		/* Methods */
		// �\�[�_�p���[�̎w��
		public void SetPower(float targetPower)
		{
			power = targetPower;

			if (power > maxPower) {
				power = maxPower;
			}
		}

		// �p���[���炷
		void RemovePower()
		{
			if (power > 0) {
				power -= removePowerAmount;

				if(power < 0) {
					power = 0;
				}
			}
		}
	}
}