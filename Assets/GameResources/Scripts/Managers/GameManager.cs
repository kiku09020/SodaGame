using Cinemachine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameController.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameController.Manager {
	public class GameManager : ObjectCore {
		/* Properties */
		/// <summary> ゲームオーバーフラグ </summary>
		public static bool IsGameOvered { get; private set; }

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
			if (IsGameOvered) {

				if (!once) {
					once = true;
					Camera.main.transform.DOShakePosition(1);

					UIManager.HideAllUIGroups();


					await UniTask.Delay(1000);

					UIManager.ShowUIGroup<GameOverUIGroup>();
					IsResult = true;
				}
			}
		}

		//--------------------------------------------------
		/* Setters */
		/// <summary> ゲームオーバー </summary>
		public static void SetGameOvered() { IsGameOvered = true; }
	}
}