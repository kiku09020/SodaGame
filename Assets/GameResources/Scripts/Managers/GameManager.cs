using Cinemachine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameController.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameController.Manager {
	public class GameManager : ObjectCore {
		[SerializeField] SEManager systemSoundsManager;

		/* Properties */
		/// <summary> �Q�[���I�[�o�[�t���O </summary>
		public static bool IsGameOvered { get; private set; }
		/// <summary> ���U���g�t���O </summary>
		public static bool IsResult { get; private set; }

		bool once;

		//--------------------------------------------------

		void Start()
		{
			PauseManager.UnPause();

			IsGameOvered = false;
			IsResult = false;
		}

		private async void FixedUpdate()
		{
			if (IsGameOvered && !once) {
				once = true;

				await systemSoundsManager.PlayAudio("Dead");        // ���ʉ��Đ�
				Camera.main.transform.DOShakePosition(1);           // �J�����h�炷
				UIManager.HideAllUIGroups();                        // UI��\��

				// �ҋ@
				await UniTask.Delay(1000);

				UIManager.ShowUIGroup<GameOverUIGroup>();           // ���U���g��ʕ\��
				IsResult = true;

				await systemSoundsManager.PlayAudio("Tada");
			}
		}

		//--------------------------------------------------
		/* Setters */
		/// <summary> �Q�[���I�[�o�[ </summary>
		public static void SetGameOvered() { IsGameOvered = true; }
	}
}