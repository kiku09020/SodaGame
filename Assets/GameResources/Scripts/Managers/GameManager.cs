using Cinemachine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameController.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameController.Manager {
	public class GameManager : ObjectCore {

		[SerializeField] Camera cam;

		/* Properties */
		/// <summary> ゲームクリアフラグ </summary>
		public static bool IsGameCleared { get; private set; }
		/// <summary> ゲームオーバーフラグ </summary>
		public static bool IsGameOvered { get; private set; }

		bool once;

		//--------------------------------------------------

		void Start()
		{
			PauseManager.UnPause();

			IsGameCleared = false;
			IsGameOvered = false;
		}

		private async void FixedUpdate()
		{
			if (IsGameOvered) {

				if (!once) {
					once = true;
					cam.transform.DOShakePosition(1);

					UIManager.HideAllUIGroups();


					await UniTask.Delay(1000);

					UIManager.ShowUIGroup<GameOverUIGroup>();
				}
			}
		}

		//--------------------------------------------------
		/* Setters */
		/// <summary> ゲームクリア </summary>
		public static void SetGameCleared() { IsGameCleared = true; }
		/// <summary> ゲームオーバー </summary>
		public static void SetGameOvered() { IsGameOvered = true; }
	}
}