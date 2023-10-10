using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameController.UI.Title {
	public class RankingUnit : MonoBehaviour {
		/* Fields */
		[Header("Text")]
		[SerializeField] TextMeshProUGUI rankText;
		[SerializeField] TextMeshProUGUI scoreText;

		[Header("Image")]
		[SerializeField] Image rankImage;

		//-------------------------------------------------------------------
		/* Methods */
		public void SetRankText(int rank)
		{
			rankText.text = rank.ToString();
		}

		public void SetScoreText(int score)
		{
			scoreText.text = ScoreManager.GetScoreText(score);
		}

		public void SetRankImageColor(Color color)
		{
			rankImage.color = color;
		}
	}
}