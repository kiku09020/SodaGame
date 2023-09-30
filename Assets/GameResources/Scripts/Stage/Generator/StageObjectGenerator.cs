using Base.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage {
	public class StageObjectGenerator<T> : MyObjectPool<T> where T : StageObjectBase<T> {
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

		List<T> stages = new List<T>();

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

		protected override void OnGetFromPool(T obj)
		{
			base.OnGetFromPool(obj);

			stages.Add(obj);
		}

		protected override void OnReleaseToPool(T obj)
		{
			base.OnReleaseToPool(obj);

			stages.Remove(obj);
		}

		//-------------------------------------------------------------------
		/* Methods */
		void Generate()
		{
			var obj = pool.Get();

			SetGenrerateHeight();
			SetStageTransform(obj);
		}

		// �ʒu�Ƃ��傫���w�肷��
		void SetStageTransform(T stage)
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

					if (!stage.IsReleased) {
						pool.Release(stage);
					}

					break;
				}
			}
		}
	}
}