using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameController.UI {
	public class ButtonHoldUIController : MonoBehaviour {
		/* Fields */
		[Header("Components")]
		[SerializeField] Image filledImage;

		[Header("Parameters")]
		[SerializeField] float increasedAmount = .01f;
		[SerializeField] float decreasedAmount = .02f;

		float currentAmount;

		//-------------------------------------------------------------------
		/* Properties */
		public bool IsCompleted { get; private set; }

		//-------------------------------------------------------------------
		/* Events */
		void Awake()
		{
			currentAmount = 0;
			filledImage.fillAmount = 0;
		}

		void Update()
		{
			// 押されてたら増やす
			if (PlayerController.ActiveController.IsPressed) {
				if (currentAmount < 1) {
					currentAmount += increasedAmount;
				}
				else {
					currentAmount = 1;
					IsCompleted = true;
					MySceneManager.LoadScene("Main");		// 読み込み
				}
			}

			// 自動で減らす
			else {
				if (currentAmount > 0) {
					currentAmount -= decreasedAmount;
				}
				else {
					currentAmount = 0;
				}
			}

			filledImage.fillAmount = currentAmount;
		}

		//-------------------------------------------------------------------
		/* Methods */

	}
}