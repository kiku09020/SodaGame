using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player {
	/// <summary> �U��@�\ </summary>
	public class PlayerShaker : PlayerComponent {
		/* Fields */
		[SerializeField] SpriteRenderer rend;
		[SerializeField] PlayerSodaManager sodaManager;

		[Header("Parameters")]
		[SerializeField, Tooltip("�U���Ă邩�ǂ�����臒l")]
		float shakableThreshold = 5;

		[SerializeField, Tooltip("�U���Ă���Ƃ��̃A�j���[�V�����̒l"), Range(0, 1)]
		float shakingTweenValue = .1f;
		[SerializeField, Range(0, 1)] float shakingTweenDuration = .1f;
		[SerializeField] Ease tweenEase;

		[Header("Debug")]
		[SerializeField] float keyBoardSodaAmount = 1;

		/// <summary> �V�F�C�N���� </summary>
		public bool IsShaking { get; private set; }

		bool isShakeFirst;

		float shakePower;       // �ǂ񂾂��U������

		Tween shakeTween;

		//-------------------------------------------------------------------
		/* Properties */
		//-------------------------------------------------------------------
		/* Methods */
		public void Shake()
		{
			float shakeAmount = 0;

			// m5
			if (PlayerController.ActiveCtrlIsDevice) {
				// �{�^���������Ă����ԂŁA�����x�̑傫����臒l�ȏ�̒l�ȏゾ������A�U���Ă��锻��ɂ���
				IsShaking = PlayerController.ActiveController.IsPressed &&
							DeviceDataReceiver.Acc.magnitude > shakableThreshold;

				shakeAmount = DeviceDataReceiver.Acc.magnitude;
			}

			// keyboard
			else {
				IsShaking = PlayerController.ActiveController.IsPressed;
				shakeAmount = keyBoardSodaAmount;
			}

			if (IsShaking) {
				// �U��n��
				if (!isShakeFirst) {
					OnShakeMoment();
				}

				// �X�V����
				ShakeUpdate(shakeAmount);
			}

			// �����ꂽ�Ƃ��̏���
			else if (!PlayerController.ActiveController.IsPressed) {
				if (isShakeFirst) {
					OnStopShakeMoment();
				}

				stateMachine.StateTransition("Soda");       // �ʏ��ԂɑJ��
			}

			if (isDebug) {
				print(shakePower);
			}
		}

		// �U��͂��߂��u�Ԃ̏���
		void OnShakeMoment()
		{
			isShakeFirst = true;

			// �V�F�C�N�A�j���[�V����
			shakeTween = rend.transform.DOShakePosition(shakingTweenDuration, shakingTweenValue)
				.SetEase(tweenEase)
				.SetLoops(-1, LoopType.Yoyo);
		}

		// �U��̂��~�߂��u�Ԃ̏���
		void OnStopShakeMoment()
		{
			isShakeFirst = false;
			shakePower = 0;

			// �V�F�C�N�A�j���[�V�������~�߂�
			shakeTween.Kill();
			rend.transform.DOLocalMove(Vector2.zero, 0);
		}

		// �U���Ă���Ƃ��̍X�V����
		void ShakeUpdate(float shakeAmount)
		{
			// �f�o�C�X�̉����x�����Z
			shakePower += shakeAmount;

			// �\�[�_�p���[�ɔ��f
			sodaManager.SetPower(shakePower);

		}
	}
}