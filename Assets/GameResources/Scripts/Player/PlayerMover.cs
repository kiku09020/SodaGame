using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player {
    /// <summary> �v���C���[�̈ړ����� </summary>
    public class PlayerMover : PlayerComponent {
        /* Fields */

        [Header("Components")]
        [SerializeField] Rigidbody2D rb;

        [Header("Parameters")]
        [SerializeField] float speed = 50;
        [SerializeField] float maxSpeed = 100;

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */
		protected override void OnFixedUpdate()
		{
            Move();
		}

		//-------------------------------------------------------------------
		/* Methods */
		void Move()
        {
            // �{�^�����͒��͈ړ����Ȃ�
            if (!PlayerController.ActiveController.IsPressed) {

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
                rb.velocity = Vector2.zero;
            }
        }
    }
}