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
		[SerializeField] float cameraShakingValue = 5;
		[SerializeField] float cameraShakingDuration = .2f;
		[SerializeField] Ease cameraShakingEase;

		Tween shakingCameraTween;

        //-------------------------------------------------------------------
        /* Methods */
		public void CameraShaking(float shakingAmount)
		{
			var fixedShakingAmount = shakingAmount * cameraShakingValue;

			// ���X����������A�E�ɌX����
			if (cam.m_Lens.Dutch <= 0) {
				shakingCameraTween = DOVirtual.Float(cam.m_Lens.Dutch, fixedShakingAmount, cameraShakingDuration, value => {
					cam.m_Lens.Dutch = value;
				}).SetEase(cameraShakingEase);
			}

			// �E�X����������A����
			else {
				shakingCameraTween = DOVirtual.Float(cam.m_Lens.Dutch, -fixedShakingAmount, cameraShakingDuration, value => {
					cam.m_Lens.Dutch = value;
				}).SetEase(cameraShakingEase);
			}


		}

		public void CameraShakingEnd()
		{
			shakingCameraTween.Complete();

			DOVirtual.Float(cam.m_Lens.Dutch, 0, cameraShakingDuration, value => {
				cam.m_Lens.Dutch = value;
			});
		}
    }
}