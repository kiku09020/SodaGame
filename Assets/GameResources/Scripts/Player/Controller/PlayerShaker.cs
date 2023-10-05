using Cinemachine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player {
	/// <summary> 振る機能 </summary>
	public class PlayerShaker : PlayerComponent {
		/* Fields */
		[Header("Components")]
		[SerializeField] SpriteRenderer rend;
		[SerializeField] PlayerSodaManager sodaManager;
		[SerializeField] PlayerCameraController cameraController;

		[Header("Parameters")]
		[SerializeField, Tooltip("振ってるかどうかの閾値")]
		float shakableThreshold = 5;

		[Header("Animation")]
		[SerializeField, Tooltip("振られているときのアニメーションの値"), Range(0, 1)]
		float shakingTweenValue = .1f;
		[SerializeField, Range(0, 1)] float shakingTweenDuration = .1f;
		[SerializeField] Ease tweenEase;

		[Header("Debug")]
		[SerializeField] float keyBoardSodaAmount = 1;

		bool isShakeFirst;

		Tween shakeTween;

		//-------------------------------------------------------------------
		/* Methods */
		public void Shake()
		{
			float shakeAmount;
			bool isShaking;

			if (PlayerController.ActiveCtrlIsDevice) {
				// ボタンを押している状態で、加速度の大きさが閾値以上の値以上だったら、振っている判定にする
				isShaking = PlayerController.ActiveController.IsPressed &&
							DeviceDataReceiver.Acc.magnitude > shakableThreshold;

				shakeAmount = DeviceDataReceiver.Acc.magnitude;
			}

			// keyboard
			else {
				isShaking = PlayerController.ActiveController.IsPressed;
				shakeAmount = keyBoardSodaAmount;
			}

			if (isShaking) {
				// 振り始め
				if (!isShakeFirst) {
					isShakeFirst = true;
				}

				// 更新処理
				ShakeUpdate(shakeAmount);
			}

			// 離されたときの処理
			else if (!PlayerController.ActiveController.IsPressed) {
				isShakeFirst = false;
				cameraController.CameraShakingEnd();
				stateMachine.StateTransition("Soda");       // 通常状態に遷移
			}
		}

		// 振っているときの更新処理
		void ShakeUpdate(float shakeAmount)
		{
			// ソーダパワーに反映
			sodaManager.AddPower(shakeAmount);

			// カメラ揺らす
			cameraController.CameraShaking(shakeAmount);

			// プレイヤーアニメーション
			shakeTween.Complete();
			shakeTween = rend.transform.DOShakePosition(shakingTweenDuration, shakingTweenValue)
				.SetEase(tweenEase)
				.SetLoops(1, LoopType.Yoyo);
		}
	}
}