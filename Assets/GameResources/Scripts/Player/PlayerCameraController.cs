using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player {
    /// <summary> �J��������N���X </summary>
    public class PlayerCameraController : MonoBehaviour {
		/* Fields */
		[Header("Camera")]
		[SerializeField] CinemachineVirtualCamera cam;
		[SerializeField] float cameraShakingDutch = 5;
		[SerializeField] float cameraShakingDuration = .2f;
		[SerializeField] Ease cameraShakingEase;

		Tween shakingCameraTween;

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */

        //-------------------------------------------------------------------
        /* Methods */
		public void CameraShaking()
		{
			// �J�����U��
			shakingCameraTween = DOVirtual.Float(-cameraShakingDutch, cameraShakingDutch, cameraShakingDuration, value => {
				cam.m_Lens.Dutch = value;
			}).SetLoops(-1, LoopType.Yoyo).SetEase(cameraShakingEase);
		}

		public void CameraShakingEnd()
		{
			// �J�����X���߂�
			shakingCameraTween.Kill();

			DOVirtual.Float(cam.m_Lens.Dutch, 0, cameraShakingDuration, value => {
				cam.m_Lens.Dutch = value;
			});
		}
    }
}