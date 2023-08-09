using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolBase<T> : MonoBehaviour where T:PooledObject<T>
{
	[Header("ObjectPool")]
	[SerializeField] protected T prefab;			// 生成するオブジェクトのプレハブ
	[SerializeField] Transform generatedParent;     // 生成される親Transform		

	[Space(10)]
	[SerializeField] bool checkCollection = true;
	[SerializeField] int defaultCapacity = 20;
	[SerializeField] int maxSize = 100;

	protected ObjectPool<T> pool;

	/// <summary> プールの取得 </summary>
	public ObjectPool<T> GetPool() => pool;

	//--------------------------------------------------
	protected void Start()
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

	/// <summary> オブジェクトをプールから取得するときの処理 </summary>
	protected virtual void OnGetFromPool(T obj)
	{
		obj.OnGetted();
	}


	/// <summary> オブジェクトをプールに返すときの処理 </summary>
	protected virtual void OnReleaseToPool(T obj)
	{
		obj.OnReleased();
	}

	/// <summary> プール内のオブジェクトを削除するときの処理 </summary>
	protected virtual void OnDestroyObject(T obj)
	{
		Destroy(obj.gameObject);
	}
}
