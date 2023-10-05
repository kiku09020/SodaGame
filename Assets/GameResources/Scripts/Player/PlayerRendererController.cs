using Cinemachine;
using GameController.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player {
	public class PlayerRendererController : MonoBehaviour {
		/* Fields */
		[SerializeField] SpriteRenderer rend;
		[SerializeField] CinemachineVirtualCamera virtualCamera;
		[SerializeField] ParticleSystem deadEffect;

		[SerializeField] PlayerCore player;

		[SerializeField] List<Sprite> faceSprites = new List<Sprite>();

		bool isInvisibled = false;

		public enum PlayerFace {
			normal ,
			shaked,
			splashing,
		}

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */

		// 画面外
		private void OnBecameInvisible()
		{
			GameOverPorcess();

		}

		//-------------------------------------------------------------------
		/* Methods */
		public void GameOverPorcess()
		{
			if (!isInvisibled) {
				isInvisibled = true;
				virtualCamera.enabled = false;
				Instantiate(deadEffect, transform.position, Quaternion.identity);

				NAudioController.Play("Damaged");

				// ゲームオーバーにする
				GameManager.SetGameOvered();

				player.gameObject.SetActive(false);
			}
		}

		public void ChangeFace(PlayerFace faceType)
		{
			rend.sprite = faceSprites[(int)faceType];
		}
	}
}