using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player {
    /// <summary> プレイヤーの移動処理 </summary>
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
            // ボタン入力中は移動しない
            if (!PlayerController.ActiveController.IsPressed) {

                // 移動
                if (rb.velocity.magnitude < maxSpeed) {
                    rb.AddForce(Vector2.left * PlayerController.ActiveController.AxisX * speed);

                }

                if (isDebug) {

                    print($"velocity: {rb.velocity.magnitude}");
                }
            }

            // ボタン入力されたら停止
            else {
                rb.velocity = Vector2.zero;
            }
        }
    }
}