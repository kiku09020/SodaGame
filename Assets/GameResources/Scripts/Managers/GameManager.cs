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
		/// <summary> ゲームオーバーフラグ </summary>
		public static bool IsGameOvered { get; private set; }
		/// <summary> リザルトフラグ </summary>
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

				await systemSoundsManager.PlayAudio("Dead");        // 効果音再生
				Camera.main.transform.DOShakePosition(1);           // カメラ揺らす
				UIManager.HideAllUIGroups();                        // UI非表示

				// 待機
				await UniTask.Delay(1000);

				UIManager.ShowUIGroup<GameOverUIGroup>();           // リザルト画面表示
				IsResult = true;

				await systemSoundsManager.PlayAudio("Tada");
			}
		}

		//--------------------------------------------------
		/* Setters */
		/// <summary> ゲームオーバー </summary>
		public static void SetGameOvered() { IsGameOvered = true; }
	}
}