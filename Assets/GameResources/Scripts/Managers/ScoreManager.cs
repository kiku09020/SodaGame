using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Player;
using GameController.Manager;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GameController {
	public class ScoreManager : MonoBehaviour {
		/* Fields */
		[SerializeField] TextMeshProUGUI scoreText;             // ゲーム中のスコアテキスト
		[SerializeField] TextMeshProUGUI resultScoreText;       // 結果スコアテキスト
		[SerializeField] TextMeshProUGUI highScoreText;        // ハイスコアテキスト
		[SerializeField] TextMeshProUGUI highScoreLabel;

		[SerializeField] PlayerCore player;
		[SerializeField] SEManager systemSoundManager;

		[SerializeField] float resultScoreDuration = .5f;
		[SerializeField] Ease resultScoreEase;

		[Header("HighScore")]
		[SerializeField] float highScoreInValue = .3f;
		[SerializeField] float highScoreOutValue = 1;
		[SerializeField] float highScoreInDuration = .9f;
		[SerializeField] float highScoreOutDuration = .1f;
		[SerializeField] Ease highScoreInEase;
		[SerializeField] Ease highScoreOutEase;

		[SerializeField] float highScoreJumpPower = 5;
		[SerializeField] float highScoreJumpDuration = .5f;

		[SerializeField] Color highScoreColor = Color.yellow;

		int currentScore;
		int resultScore;
		int highScore;

		bool resultOnce;

		//-------------------------------------------------------------------
		/* Properties */
		string ScoreText => $"Score:{currentScore}m";

		//-------------------------------------------------------------------
		/* Events */

		void FixedUpdate()
		{
			currentScore = (int)player.transform.position.y;
			scoreText.text = ScoreText;

			// ゲームオーバー時に、結果スコアを適用
			if (GameManager.IsResult && !resultOnce) {
				resultOnce = true;

				DOVirtual.Float(0, currentScore, resultScoreDuration, value => {
					resultScore = (int)value;
					resultScoreText.text = $"{resultScore}m";
				})
					.SetEase(resultScoreEase);

				SetHighScore();
			}
		}

		//-------------------------------------------------------------------
		/* Methods */
		// ハイスコア更新
		async void SetHighScore()
		{
			// ハイスコア適用
			highScore = PlayerPrefs.GetInt("HighScore", 0);
			highScoreText.text = $"{highScore}m";

			// ハイスコアより高ければ、ハイスコアに適用
			if (currentScore > highScore) {
				highScore = currentScore;
				PlayerPrefs.SetInt("HighScore", highScore);

				// ハイスコア更新アニメーション
				Sequence seq = DOTween.Sequence();

				seq.Append(highScoreText.transform.DOScale(highScoreInValue, highScoreInDuration).SetEase(highScoreInEase));
				seq.Append(highScoreText.transform.DOScale(highScoreOutValue, highScoreOutDuration).SetEase(highScoreOutEase));

				seq.Play();
				await UniTask.Delay(System.TimeSpan.FromSeconds(highScoreInDuration));

				// ハイスコアぴょんぴょん
				highScoreLabel.transform.DOLocalJump(highScoreLabel.transform.localPosition, highScoreJumpPower, 1, highScoreJumpDuration).SetLoops(-1);
				highScoreText.transform.DOLocalJump(Vector2.zero, highScoreJumpPower, 1, highScoreJumpDuration).SetLoops(-1);

				// ラベル文字列変更
				highScoreLabel.text = "HighScore!";

				// 文字色変更
				highScoreText.color = highScoreColor;
				highScoreLabel.color = highScoreColor;

				// 効果音再生
				await systemSoundManager.PlayAudio("HighScore");
			}

			highScoreText.text = $"{highScore}m";
		}
	}
}