using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player {
	/// <summary> 振る機能 </summary>
	public class PlayerShaker : PlayerComponent {
		/* Fields */

		[Header("Components")]
		[SerializeField] PlayerSodaManager sodaManager;

		[Header("Parameters")]
		[SerializeField, Tooltip("振ってるかどうかの閾値")] float shakableThreshold = 5;

		/// <summary> シェイク中か </summary>
		public bool IsShaking { get; private set; }

		float shakePower;       // どんだけ振ったか

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
			// ボタンを押している状態で、加速度の大きさが閾値以上の値以上だったら、振っている判定にする
			IsShaking = PlayerController.ActiveController.IsPressed &&
						DeviceDataReceiver.Acc.magnitude > shakableThreshold;

			if (IsShaking) {
				// デバイスの加速度を加算
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