using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Base.Pool
{
	public abstract class MyObjectPool<T> : ObjectPoolBase<T> where T : PooledObject<T>
	{
		[SerializeField] protected T prefab;            // 生成するオブジェクトのプレハブ
		[SerializeField] Transform generatedParent;     // 生成される親Transform		

		protected ObjectPool<T> pool;

		/// <summary> プールの取得 </summary>
		public ObjectPool<T> GetPool() => pool;

		//--------------------------------------------------
		protected override void CreatePool()
		{
			pool = new ObjectPool<T>(OnCreateObject, OnGetFromPool, OnReleaseToPool, OnDestroyObject,
				checkCollection, defaultCapacity, maxSize);
		}

		/// <summary> オブジェクト作成時の処理 </summary>
		protected virtual T OnCreateObject()
		{
			T obj = Instantiate(prefab, generatedParent);
			obj.OnCreated(pool);
			obj.OnGetted();

			return obj;
		}

		protected override void OnGetFromPool(T obj)
		{
			obj.gameObject.SetActive(true);
			obj.OnGetted();
		}

		protected override void OnReleaseToPool(T obj)
		{
			obj.gameObject.SetActive(false);
			obj.OnReleased();
		}

		protected override void OnDestroyObject(T obj)
		{
			Destroy(obj.gameObject);
		}
	}
}