using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Base.Pool
{
	public abstract class MyObjectPool<T> : ObjectPoolBase<T> where T : PooledObject<T>
	{
		[SerializeField] protected T prefab;            // ��������I�u�W�F�N�g�̃v���n�u
		[SerializeField] Transform generatedParent;     // ���������eTransform		

		protected ObjectPool<T> pool;

		/// <summary> �v�[���̎擾 </summary>
		public ObjectPool<T> GetPool() => pool;

		//--------------------------------------------------
		protected override void CreatePool()
		{
			pool = new ObjectPool<T>(OnCreateObject, OnGetFromPool, OnReleaseToPool, OnDestroyObject,
				checkCollection, defaultCapacity, maxSize);
		}

		/// <summary> �I�u�W�F�N�g�쐬���̏��� </summary>
		protected virtual T OnCreateObject()
		{
			T obj = Instantiate(prefab, generatedParent);
			obj.OnCreated(pool);
			obj.OnGetted();

			return obj;
		}
	}
}