using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage {
	/// <summary> �X�e�[�W�̍����ɂ���āA��Փx�ω��������Փx�Ǘ��N���X </summary>
	public class StageLevelChecker : MonoBehaviour {
		/* Fields */
		[SerializeField,Tooltip("���x�����オ�鍂���̊Ԋu")] 
		float levelUpHeightDuration = 100;

		float currentHeight;
		float targetHeight;

		//-------------------------------------------------------------------
		/* Properties */
		/// <summary> ��Փx���x�� </summary>
		public static int DifficultyLevel { get; private set; }

		//-------------------------------------------------------------------
		/* Events */
		void Awake()
		{
			DifficultyLevel = 0;
			targetHeight += levelUpHeightDuration;
		}

		void FixedUpdate()
		{
			CheckHeight();
		}

		//-------------------------------------------------------------------
		/* Methods */
		// �����`�F�b�N
		void CheckHeight()
		{
			// �����擾
			currentHeight =  Camera.main.transform.position.y;

			// �ڕW�̍���������ɗ���΁A���x���A�b�v
			if (currentHeight >= targetHeight) {
				DifficultyLevel++;
				targetHeight += levelUpHeightDuration;
			}
		}
	}
}