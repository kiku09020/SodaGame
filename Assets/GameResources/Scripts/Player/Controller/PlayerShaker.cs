using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player {
	/// <summary> 振る機能 </summary>
	public class PlayerShaker : PlayerComponent {
		/* Fields */
		[SerializeField] SpriteRenderer rend;
		[SerializeField] PlayerSodaManager sodaManager;

		[Header("Parameters")]
		[SerializeField, Tooltip("振ってるかどうかの閾値")]
		float shakableThreshold = 5;
		[SerializeField, Tooltip("ShakePowerの最大値")]
		float maxShakePower = 150;

		[SerializeField, Tooltip("振られているときのアニメーションの値"), Range(0, 1)]
		float shakingTweenValue = .1f;
		[SerializeField, Range(0, 1)] float shakingTweenDuration = .1f;
		[SerializeField] Ease tweenEase;

		/// <summary> シェイク中か </summary>
		public bool IsShaking { get; private set; }

		bool isShakeFirst;

		float shakePower;       // どんだけ振ったか

		Tween shakeTween;

		//-------------------------------------------------------------------
		/* Properties */
		public float ShakePower => shakePower;

		//-------------------------------------------------------------------
		/* Methods */
		public void Shake()
		{
			// ボタンを押している状態で、加速度の大きさが閾値以上の値以上だったら、振っている判定にする
			IsShaking = PlayerController.ActiveController.IsPressed &&
						DeviceDataReceiver.Acc.magnitude > shakableThreshold;

			if (IsShaking) {
				if (!isShakeFirst) {
					OnShakeMoment();
				}

				ShakeUpdate();
			}

			// 離されたときの処理
			else if (!PlayerController.ActiveController.IsPressed) {
				if (isShakeFirst) {
					OnStopShakeMoment();
				}

				stateMachine.StateTransition("Soda");		// 通常状態に遷移
			}

			if (isDebug) {
				print(shakePower);
			}
		}

		// 振りはじめた瞬間の処理
		void OnShakeMoment()
		{
			isShakeFirst = true;

			// シェイクアニメーション
			shakeTween = rend.transform.DOShakePosition(shakingTweenDuration, shakingTweenValue)
				.SetEase(tweenEase)
				.SetLoops(-1, LoopType.Yoyo);
		}

		// 振るのを止めた瞬間の処理
		void OnStopShakeMoment()
		{
			isShakeFirst = false;
			shakePower = 0;

			// シェイクアニメーションを止める
			shakeTween.Kill();
			rend.transform.DOLocalMove(Vector2.zero, 0);
		}

		// 振っているときの更新処理
		void ShakeUpdate()
		{
			// デバイスの加速度を加算
			if (shakePower < maxShakePower) {
				shakePower += DeviceDataReceiver.Acc.magnitude;
			}
		}
	}
}