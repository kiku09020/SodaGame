using Base.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage {
	public class StageGenerator : MyObjectPool<Stage> {
		/* Fields */
		[Header("Parameters")]
		[SerializeField] float xRange = 3;
		[SerializeField, Tooltip("�����ʒu����J�����ʒu�̋���")]
		float generateDistance = 10;
		[SerializeField, Tooltip("�J�����ʒu����̎��̐����ʒu�܂ł̋���")] 
		float generateOffset = 15;

		[SerializeField] float destroyDistance = 10;

		[SerializeField] float minWidth = 3;
		[SerializeField] float maxWidth = 6;

		float generateY;                // ����Y�ʒu

		float currentCameraPosY;        // ���݂̃J������Y�ʒu

		List<Stage> stages = new List<Stage>();

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */
		private void FixedUpdate()
		{
			// �J�����ʒu�X�V
			currentCameraPosY = Camera.main.transform.position.y;

			// �����ʒu�ƌ��݂̃J�����̈ʒu�̍����w�苗�����߂Â��ΐ���
			if ((generateY - currentCameraPosY) < generateDistance) {
				Generate();
			}

			// �폜
			DestroyGeneratedObj();
		}

		//-------------------------------------------------------------------
		/* Methods */
		public void Generate()
		{
			var obj = pool.Get();

			SetGenrerateHeight();
			SetStageTransform(obj);

			stages.Add(obj);
		}

		// �ʒu�Ƃ��傫���w�肷��
		void SetStageTransform(Stage stage)
		{
			// �ʒu�w��
			float randomX = Random.Range(-xRange, xRange);

			stage.transform.position = new Vector2(randomX, 10);

			// ���w��
			float randomWidth = Random.Range(minWidth, maxWidth);
			stage.SetWidth(randomWidth);

			stage.transform.position = new Vector2(randomX, generateY);
		}

		void SetGenrerateHeight()
		{
			// �J������Y���W+�I�t�Z�b�g�ʒu
			generateY = currentCameraPosY + generateOffset;
		}

		void DestroyGeneratedObj()
		{
			foreach (var stage in stages) {
				if ((currentCameraPosY - stage.transform.position.y) > destroyDistance &&
					currentCameraPosY > stage.transform.position.y) {

					stages.Remove(stage);
					pool.Release(stage);

					break;
				}
			}
		}
	}
}