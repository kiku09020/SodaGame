using Cinemachine;
using GameController.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player {
	public class PlayerRendererController : MonoBehaviour {
		/* Fields */
		[SerializeField] CinemachineVirtualCamera virtualCamera;
		[SerializeField] ParticleSystem deadEffect;

		[SerializeField] PlayerCore player;

		bool isInvisibled = false;

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */

		// ��ʊO
		private void OnBecameInvisible()
		{
			if (!isInvisibled) {
				isInvisibled = true;
				virtualCamera.enabled = false;
				Instantiate(deadEffect,transform.position,Quaternion.identity);

				// �Q�[���I�[�o�[�ɂ���
				GameManager.SetGameOvered();

				player.gameObject.SetActive(false);
			}

		}

		//-------------------------------------------------------------------
		/* Methods */

	}
}