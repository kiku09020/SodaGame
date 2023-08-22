using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player {
	/// <summary> �v���C���[�̒ʏ펞�̈ړ����� </summary>
	public class PlayerRollingMover : PlayerComponent {
		/* Fields */

		[SerializeField] Rigidbody2D rb;

		[Header("Parameters")]
		[SerializeField] float speed = 50;
		[SerializeField] float maxSpeed = 100;

		[SerializeField, Tooltip("���̓u���[�L臒l")]
		float breakThreshold = .75f;
		[SerializeField, Tooltip("�u���[�L���̌����l"), Range(0, 1)]
		float breakSpeedValue = .5f;

		float prevX;     // �O�t���[���̍��E����

		bool moveFirst;     // �ړ������u��

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */

		//-------------------------------------------------------------------
		/* Methods */
		public void Move()
		{
			// �{�^�����͂���Ă��Ȃ���Έړ�
			if (!PlayerController.ActiveController.IsPressed) {

				// �ړ������u��
				if (!moveFirst) {
					moveFirst = true;
					rb.constraints = RigidbodyConstraints2D.None;
				}

				// �u���[�L
				if (Mathf.Abs(prevX - PlayerController.ActiveController.AxisX) > breakThreshold) {
					rb.velocity *= breakSpeedValue;
				}

				// �ړ�
				if (rb.velocity.magnitude < maxSpeed) {
					rb.AddForce(Vector2.left * PlayerController.ActiveController.AxisX * speed);

				}

				if (isDebug) {

					print($"velocity: {rb.velocity.magnitude}");
				}
			}

			// �{�^�����͂��ꂽ���~
			else {
				OnStopMoment();
			}

			// ���͕ۑ�
			prevX = PlayerController.ActiveController.AxisX;
		}

		// ��~�����u��
		void OnStopMoment()
		{
			if (moveFirst) {
				moveFirst = false;
				rb.constraints = RigidbodyConstraints2D.FreezeRotation;
				rb.velocity = Vector2.zero;

				stateMachine.StateTransition("Shaking");
			}
		}
	}
}