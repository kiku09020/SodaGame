using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player {

	/// <summary> 炭酸噴出した移動処理 </summary>
	public class PlayerSodaMover : PlayerComponent {
		/* Fields */
		[SerializeField] Rigidbody2D rb;

		[Header("Parameters")]
		[SerializeField] float startSodaPower = 10;

		[SerializeField, Tooltip("横移動")] float moveSidePower = 1;
		[SerializeField, Tooltip("炭酸噴出力")] float sodaPower = 5;

		[SerializeField, Tooltip("最大横移動速度")] float moveMaxVel = 7.5f;
		[SerializeField, Tooltip("最大噴出力")] float sodaMaxVel = 10;

		[SerializeField, Tooltip("最大回転角度")] float rotateRange = 30;
		[SerializeField, Tooltip("1Fあたりの回転角度")] float rotateDegree = .1f;

		[SerializeField, Tooltip("入力ブレーキ閾値")]
		float breakThreshold = .75f;
		[SerializeField, Tooltip("ブレーキ時の減速値"), Range(0, 1)]
		float breakSpeedValue = .5f;

		float prevX;     // 前フレームの左右入力


		public bool isFirstSoda;        // 最初

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Methods */

		/// <summary> 炭酸噴出移動 </summary>
		public void MoveWithSoda()
		{
			if (!isFirstSoda) {
				isFirstSoda = true;

				// 最初に吹っ飛び
				rb.AddForce(core.transform.right * -1 * startSodaPower, ForceMode2D.Impulse);
			}

			// 炭酸噴出
			if (rb.velocity.y < sodaMaxVel) {
				rb.AddForce(core.transform.right * -1 * sodaPower);
			}

			// 横移動
			if (rb.velocity.x < moveMaxVel) {

				// ブレーキ
				if (Mathf.Abs(prevX - PlayerController.ActiveController.AxisX) > breakThreshold) {
					rb.velocity *= breakSpeedValue;
				}

				// 移動
				rb.AddForce(Vector2.left * PlayerController.ActiveController.AxisX * moveSidePower);

				// 回転させる
				if (Mathf.Abs(core.transform.rotation.z) < rotateRange) {
					float degree = rotateDegree * PlayerController.ActiveController.AxisX;

					core.transform.Rotate(new Vector3(0, 0, degree));
				}
			}

			// ボタン押されたら、通常状態に遷移する
			if (PlayerController.ActiveController.IsPressed) {
				stateMachine.StateTransition("Normal");
			}

			if (isDebug) {
				print("vel:" + rb.velocity);
			}

			// 入力保存
			prevX = PlayerController.ActiveController.AxisX;
		}
	}
}