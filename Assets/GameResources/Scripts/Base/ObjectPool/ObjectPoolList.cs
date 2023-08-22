using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Base.Pool
{
	public abstract class ObjectPoolList<T> : ObjectPoolBase<T> where T : PooledObject<T>
	{
		/* Fields */
		List<T> objectList;

		protected List<ObjectPool<T>> poolList = new List<ObjectPool<T>>();

		//-------------------------------------------------------------------
		/* Methods */
		protected override void CreatePool()
		{
			for (int i = 0; i < objectList.Count; i++) {
				poolList.Add(new ObjectPool<T>(() => OnCreate(i), OnGetFromPool, OnReleaseToPool, OnDestroyObject,
												checkCollection, defaultCapacity, maxSize));
			}
		}

		protected virtual T OnCreate(int index)
		{
			T obj = Instantiate(objectList[index]);
			obj.OnCreated(poolList[index]);
			obj.OnGetted();

			return obj;
		}

		protected override void OnGetFromPool(T obj)
		{
			obj.OnGetted();
		}

		protected override void OnReleaseToPool(T obj)
		{
			obj.OnReleased();
		}

		protected override void OnDestroyObject(T obj)
		{
			Destroy(obj.gameObject);
		}

		//-------------------------------------------------------------------
		/// <summary> プールされるオブジェクトを追加する </summary>
		protected void AddObjList(T obj)
		{
			objectList.Add(obj);
		}
	}
}