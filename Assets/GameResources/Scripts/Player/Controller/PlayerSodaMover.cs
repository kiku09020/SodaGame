using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player {

	/// <summary> �Y�_���o�����ړ����� </summary>
	public class PlayerSodaMover : PlayerComponent {
		/* Fields */
		[SerializeField] Rigidbody2D rb;

		[Header("Parameters")]
		[SerializeField] float startSodaPower = 10;

		[SerializeField, Tooltip("���ړ�")] float moveSidePower = 1;
		[SerializeField, Tooltip("�Y�_���o��")] float sodaPower = 5;

		[SerializeField, Tooltip("�ő剡�ړ����x")] float moveMaxVel = 7.5f;
		[SerializeField, Tooltip("�ő啬�o��")] float sodaMaxVel = 10;

		[SerializeField, Tooltip("�ő��]�p�x")] float rotateRange = 30;
		[SerializeField, Tooltip("1F������̉�]�p�x")] float rotateDegree = .1f;

		[SerializeField, Tooltip("���̓u���[�L臒l")]
		float breakThreshold = .75f;
		[SerializeField, Tooltip("�u���[�L���̌����l"), Range(0, 1)]
		float breakSpeedValue = .5f;

		float prevX;     // �O�t���[���̍��E����


		public bool isFirstSoda;        // �ŏ�

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Methods */

		/// <summary> �Y�_���o�ړ� </summary>
		public void MoveWithSoda()
		{
			if (!isFirstSoda) {
				isFirstSoda = true;

				// �ŏ��ɐ������
				rb.AddForce(core.transform.right * -1 * startSodaPower, ForceMode2D.Impulse);
			}

			// �Y�_���o
			if (rb.velocity.y < sodaMaxVel) {
				rb.AddForce(core.transform.right * -1 * sodaPower);
			}

			// ���ړ�
			if (rb.velocity.x < moveMaxVel) {

				// �u���[�L
				if (Mathf.Abs(prevX - PlayerController.ActiveController.AxisX) > breakThreshold) {
					rb.velocity *= breakSpeedValue;
				}

				// �ړ�
				rb.AddForce(Vector2.left * PlayerController.ActiveController.AxisX * moveSidePower);

				// ��]������
				if (Mathf.Abs(core.transform.rotation.z) < rotateRange) {
					float degree = rotateDegree * PlayerController.ActiveController.AxisX;

					core.transform.Rotate(new Vector3(0, 0, degree));
				}
			}

			// �{�^�������ꂽ��A�ʏ��ԂɑJ�ڂ���
			if (PlayerController.ActiveController.IsPressed) {
				stateMachine.StateTransition("Normal");
			}

			if (isDebug) {
				print("vel:" + rb.velocity);
			}

			// ���͕ۑ�
			prevX = PlayerController.ActiveController.AxisX;
		}
	}
}