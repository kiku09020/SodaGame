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
		[SerializeField] TextMeshProUGUI scoreText;             // �Q�[�����̃X�R�A�e�L�X�g
		[SerializeField] TextMeshProUGUI resultScoreText;       // ���ʃX�R�A�e�L�X�g
		[SerializeField] TextMeshProUGUI highScoreText;        // �n�C�X�R�A�e�L�X�g
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

			// �Q�[���I�[�o�[���ɁA���ʃX�R�A��K�p
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
		// �n�C�X�R�A�X�V
		async void SetHighScore()
		{
			// �n�C�X�R�A�K�p
			highScore = PlayerPrefs.GetInt("HighScore", 0);
			highScoreText.text = $"{highScore}m";

			// �n�C�X�R�A��荂����΁A�n�C�X�R�A�ɓK�p
			if (currentScore > highScore) {
				highScore = currentScore;
				PlayerPrefs.SetInt("HighScore", highScore);

				// �n�C�X�R�A�X�V�A�j���[�V����
				Sequence seq = DOTween.Sequence();

				seq.Append(highScoreText.transform.DOScale(highScoreInValue, highScoreInDuration).SetEase(highScoreInEase));
				seq.Append(highScoreText.transform.DOScale(highScoreOutValue, highScoreOutDuration).SetEase(highScoreOutEase));

				seq.Play();
				await UniTask.Delay(System.TimeSpan.FromSeconds(highScoreInDuration));

				// �n�C�X�R�A�҂��҂��
				highScoreLabel.transform.DOLocalJump(highScoreLabel.transform.localPosition, highScoreJumpPower, 1, highScoreJumpDuration).SetLoops(-1);
				highScoreText.transform.DOLocalJump(Vector2.zero, highScoreJumpPower, 1, highScoreJumpDuration).SetLoops(-1);

				// ���x��������ύX
				highScoreLabel.text = "HighScore!";

				// �����F�ύX
				highScoreText.color = highScoreColor;
				highScoreLabel.color = highScoreColor;

				// ���ʉ��Đ�
				await systemSoundManager.PlayAudio("HighScore");
			}

			highScoreText.text = $"{highScore}m";
		}
	}
}