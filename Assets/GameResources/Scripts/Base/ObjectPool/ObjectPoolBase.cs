using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Pool
{
	public abstract class ObjectPoolBase<T> : MonoBehaviour where T : PooledObject<T>
	{
		/* Fields */
		[Header("ObjectPool")]
		[SerializeField, Tooltip("��d����̃`�F�b�N")] protected bool checkCollection = true;
		[SerializeField, Tooltip("Stack�̏����T�C�Y")] protected int defaultCapacity = 20;
		[SerializeField, Tooltip("�v�[���̍ő�T�C�Y")] protected int maxSize = 100;

		//-------------------------------------------------------------------
		/* Events */
		void Awake()
		{
			CreatePool();
		}

		//-------------------------------------------------------------------
		/* Methods */
		/// <summary> �v�[���̍쐬 </summary>
		protected abstract void CreatePool();

		/// <summary> �I�u�W�F�N�g���v�[������擾����Ƃ��̏��� </summary>
		protected abstract void OnGetFromPool(T obj);

		/// <summary> �I�u�W�F�N�g���v�[���ɕԂ��Ƃ��̏��� </summary>
		protected abstract void OnReleaseToPool(T obj);

		/// <summary> �v�[�����̃I�u�W�F�N�g���폜����Ƃ��̏��� </summary>
		protected abstract void OnDestroyObject(T obj);

	}
}