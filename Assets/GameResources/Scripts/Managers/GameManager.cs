using Cinemachine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameController.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameController.Manager {
	public class GameManager : ObjectCore {
		[Header("Parameters")]
		[SerializeField, Tooltip("ESC�������b��")] float returnTitleDuration = 3;

		[Header("Components")]
		[SerializeField] SEManager systemSoundsManager;

		/* Properties */
		/// <summary> �Q�[���I�[�o�[�t���O </summary>
		public static bool IsGameOvered { get; private set; }
		/// <summary> ���U���g�t���O </summary>
		public static bool IsResult { get; private set; }

		bool once;

		float escTimer;

		//--------------------------------------------------

		void Start()
		{
			PauseManager.UnPause();

			IsGameOvered = false;
			IsResult = false;
		}

		private void Update()
		{
			ReturntoTitle();
		}

		private async void FixedUpdate()
		{
			if (IsGameOvered && !once) {
				once = true;

				NAudioController.Play("Dead");
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

		// �^�C�g���߂鏈��
		void ReturntoTitle()
		{
			// �^�C�}�[���Z
			if (Input.GetKey(KeyCode.Escape)) {
				escTimer += Time.deltaTime;

				// �^�C�g���ɖ߂�
				if (escTimer >= returnTitleDuration) {
					SceneManager.LoadScene("Title");
					escTimer = 0;
				}
			}

			// �^�C�}�[���Z�b�g
			if (Input.GetKeyUp(KeyCode.Escape)) {
				escTimer = 0;
			}
		}
	}
}