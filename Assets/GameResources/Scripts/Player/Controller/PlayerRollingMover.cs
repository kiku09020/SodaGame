using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player {
	/// <summary> プレイヤーの通常時の移動処理 </summary>
	public class PlayerRollingMover : PlayerComponent {
		/* Fields */
		[SerializeField] Rigidbody2D rb;

		[Header("Parameters")]
		[SerializeField] float speed = 50;
		[SerializeField] float maxSpeed = 100;

		[Header("Breaking")]
		[SerializeField] bool enableBreaking;

		[SerializeField, Tooltip("入力ブレーキ閾値")]
		float breakThreshold = .75f;
		[SerializeField, Tooltip("ブレーキ時の減速値"), Range(0, 1)]
		float breakSpeedValue = .5f;

		float prevX;     // 前フレームの左右入力
		bool moving;     // 移動した瞬間

		//-------------------------------------------------------------------
		/* Methods */
		public void Move()
		{
			// ボタン入力されていなければ移動
			if (!PlayerController.ActiveController.IsPressed) {
				// 移動した瞬間
				if (!moving) {
					moving = true;
					rb.constraints = RigidbodyConstraints2D.None;
				}

				// ブレーキ
				if (Mathf.Abs(prevX - PlayerController.ActiveController.AxisX) > breakThreshold && enableBreaking) {
					rb.velocity = new Vector2(rb.velocity.x * breakSpeedValue, rb.velocity.y);
				}

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
				OnStopMoment();
			}

			// 入力保存
			prevX = PlayerController.ActiveController.AxisX;
		}

		// 停止した瞬間
		void OnStopMoment()
		{
			if (moving && core.IsInAir) {
				moving = false;
				rb.constraints = RigidbodyConstraints2D.FreezeRotation;
				rb.velocity = new Vector2(0, rb.velocity.y);

				stateMachine.StateTransition("Shaking");
			}
		}
	}
}