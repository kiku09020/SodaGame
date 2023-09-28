using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GameController.UI {
    public class GameOverUIGroup : UIGroup {

		[SerializeField] float animDuration = .5f;
		[SerializeField] Ease animEase;

		[SerializeField] TextMeshProUGUI scoreText;

		public override void Show()
		{
			base.Show();

			ShowAnimation();
		}

		void ShowAnimation()
		{
			transform.localScale = Vector3.zero;
			transform.DOScale(Vector3.one, animDuration).SetEase(animEase);
		}

	}
}