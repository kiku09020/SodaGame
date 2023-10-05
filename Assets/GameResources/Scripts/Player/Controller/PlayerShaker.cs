using Cinemachine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player {
	/// <summary> �U��@�\ </summary>
	public class PlayerShaker : PlayerComponent {
		/* Fields */
		[Header("Components")]
		[SerializeField] SpriteRenderer rend;
		[SerializeField] PlayerSodaManager sodaManager;
		[SerializeField] PlayerCameraController cameraController;

		[Header("Parameters")]
		[SerializeField, Tooltip("�U���Ă邩�ǂ�����臒l")]
		float shakableThreshold = 5;

		[SerializeField] float shakingCoolTimeDuration = .25f;
		float shakingCoolTimer;

		[Header("Animation")]
		[SerializeField, Tooltip("�U���Ă���Ƃ��̃A�j���[�V�����̒l"), Range(0, 1)]
		float shakingTweenValue = .1f;
		[SerializeField, Range(0, 1)] float shakingTweenDuration = .1f;
		[SerializeField] Ease tweenEase;

		[Header("Debug")]
		[SerializeField] float keyBoardSodaAmount = 1;

		Tween shakeTween;

		bool isShakable;        // �U��邩

		//-------------------------------------------------------------------
		/* Methods */
		public void Shake()
		{
			float shakeAmount;
			bool isShaking;

			if (PlayerController.ActiveCtrlIsDevice) {
				// �{�^���������Ă����ԂŁA�����x�̑傫����臒l�ȏ�̒l�ȏゾ������A�U���Ă��锻��ɂ���
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
				isShakable = false;
				shakingCoolTimer += Time.deltaTime;

				if (shakingCoolTimer > shakingCoolTimeDuration) {
					isShakable = true;
					shakingCoolTimer = 0;
				}

				if (isShakable) {
					// �X�V����
					ShakeUpdate(shakeAmount);
				}
			}

			// �����ꂽ�Ƃ��̏���
			else if (!PlayerController.ActiveController.IsPressed) {
				isShakable = true;
				cameraController.CameraShakingEnd();
				stateMachine.StateTransition("Soda");       // �ʏ��ԂɑJ��
			}
		}

		// �U���Ă���Ƃ��̍X�V����
		async void ShakeUpdate(float shakeAmount)
		{
			// �\�[�_�p���[�ɔ��f
			sodaManager.AddPower(shakeAmount);

			// �J�����h�炷
			cameraController.CameraShaking(shakeAmount);

			// �v���C���[�A�j���[�V����
			shakeTween.Complete();
			shakeTween = rend.transform.DOShakePosition(shakingTweenDuration, shakingTweenValue)
				.SetEase(tweenEase)
				.SetLoops(1, LoopType.Yoyo);

			await core.SEManager.PlayAudio("Shake", true);
		}
	}
}