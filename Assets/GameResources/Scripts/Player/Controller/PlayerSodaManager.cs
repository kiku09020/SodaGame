using Game.Player.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Player {
	/// <summary> プレイヤーの炭酸の管理 </summary>
	public class PlayerSodaManager : PlayerComponent {
		/* Fields */

		[Header("Parameters")]
		[SerializeField] float maxPower = 200f;
		[SerializeField] float removePowerAmount = .2f;

		float power;		// 現在の力

		[Header("UI")]
		[SerializeField] Image filledImage;

		//-------------------------------------------------------------------
		/* Properties */
		float powerRate => power / maxPower;		// 力 / 最大力

		/// <summary> ソーダ噴き出し可能か </summary>
		public bool IsSodable { get; private set; }

		//-------------------------------------------------------------------
		/* Events */
		private void FixedUpdate()
		{
			IsSodable = (power > 0) ? true : false;		// 飛行可能フラグの判定

			filledImage.fillAmount = powerRate;			// FillAmountの反映
		}

		// ソーダ状態の時のUpdate
		public void SodaUpdate()
		{
			RemovePower();                              // パワー減らす

			// ソーダ飛行不可能になれば、通常状態に遷移する
			if (!IsSodable) {
				stateMachine.StateTransition("Normal");
			}
		}

		//-------------------------------------------------------------------
		/* Methods */
		// ソーダパワーの指定
		public void SetPower(float targetPower)
		{
			power = targetPower;

			if (power > maxPower) {
				power = maxPower;
			}
		}

		// パワー減らす
		void RemovePower()
		{
			if (power > 0) {
				power -= removePowerAmount;

				if(power < 0) {
					power = 0;
				}
			}
		}
	}
}