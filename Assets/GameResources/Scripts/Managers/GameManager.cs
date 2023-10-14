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
		[SerializeField, Tooltip("ESC長押し秒数")] float returnTitleDuration = 3;

		[Header("Components")]
		[SerializeField] SEManager systemSoundsManager;

		/* Properties */
		/// <summary> ゲームオーバーフラグ </summary>
		public static bool IsGameOvered { get; private set; }
		/// <summary> リザルトフラグ </summary>
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

		// タイトル戻る処理
		void ReturntoTitle()
		{
			// タイマー加算
			if (Input.GetKey(KeyCode.Escape)) {
				escTimer += Time.deltaTime;

				// タイトルに戻る
				if (escTimer >= returnTitleDuration) {
					SceneManager.LoadScene("Title");
					escTimer = 0;
				}
			}

			// タイマーリセット
			if (Input.GetKeyUp(KeyCode.Escape)) {
				escTimer = 0;
			}
		}
	}
}