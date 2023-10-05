using Base.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage {
	public abstract class StageObjectGeneratorBase<T> : MyObjectPool<T> where T : StageObjectBase<T> {
		/* Fields */
		[Header("Base Parameters")]
		[SerializeField, Tooltip("�����J�n�ʒu")]
		float startGenPosY = 15;
		[SerializeField, Tooltip("�J�����ʒu����Y����̍��W")]
		float genPosDistance = 10;
		[SerializeField, Tooltip("���̐����ʒu�܂ł̊Ԋu")]
		float genPosDuration = 5;

		[SerializeField, Tooltip("�J��������X�����ꂽ��폜����鋗��")]
		float destroyDistance = 10;

		[Header("Changing Size")]
		[SerializeField, Tooltip("�T�C�Y�ύX���邩")] bool isChangeSize = true;
		[SerializeField, Tooltip("�����_���ŏ��T�C�Y")] protected float randMinSize = 1;
		[SerializeField, Tooltip("�����_���ő�T�C�Y")] protected float randMaxSize = 3;

		float camPosY;              // �J������Y�ʒu
		protected float genPosY;    // ����Y�ʒu

		List<T> stageObjects = new List<T>();

		//-------------------------------------------------------------------
		/* Events */
		private void Start()
		{
			genPosY = startGenPosY;
		}

		private void FixedUpdate()
		{
			// �J�����ʒu�X�V
			camPosY = Camera.main.transform.position.y;

			// �����ʒu�ƌ��݂̃J�����̈ʒu�̍����w�苗�����߂Â��ΐ���
			if ((genPosY - camPosY) < genPosDistance) {
				Generate();

				SetGeneratePosition(genPosDuration);
			}

			// �폜
			DestroyGeneratedObj();
		}

		protected override void OnGetFromPool(T obj)
		{
			base.OnGetFromPool(obj);

			stageObjects.Add(obj);
		}

		protected override void OnReleaseToPool(T obj)
		{
			base.OnReleaseToPool(obj);

			stageObjects.Remove(obj);
		}

		//-------------------------------------------------------------------
		/* Methods */
		void Generate()
		{
			var obj = pool.Get();

			SetObjectPosition(obj);

			if (isChangeSize) {
				SetObjectSize(obj);
			}
		}

		/// <summary> �����ʒu�̎w�� </summary>
		protected virtual void SetGeneratePosition(float genDuration)
		{
			genPosY += genDuration;
		}

		// �ʒu�w��
		protected abstract void SetObjectPosition(T stage);

		/// <summary> �傫���w�� </summary>
		protected virtual void SetObjectSize(T obj)
		{
			float randomSize = Random.Range(randMinSize, randMaxSize);
			obj.SetSize(randomSize);
		}

		// �����I�u�W�F�N�g���폜
		void DestroyGeneratedObj()
		{
			foreach (var stage in stageObjects) {
				if ((camPosY - stage.transform.position.y) > destroyDistance &&
					camPosY > stage.transform.position.y) {

					if (!stage.IsReleased) {
						pool.Release(stage);
					}

					break;
				}
			}
		}
	}
}