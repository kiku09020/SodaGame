using Cinemachine;
using GameController.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player {
	public class PlayerRendererController : MonoBehaviour {
		/* Fields */
		[SerializeField] CinemachineVirtualCamera virtualCamera;

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

				// �Q�[���I�[�o�[�ɂ���
				GameManager.SetGameOvered();
			}

		}

		//-------------------------------------------------------------------
		/* Methods */

	}
}