using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PooledObject<T> : ObjectCore where T:PooledObject<T>
{
	protected IObjectPool<T> pool;      // 格納されるオブジェクトプール

	bool isReleased;

	//--------------------------------------------------
	/// <summary> 初期化 </summary>
	public virtual void OnCreated(IObjectPool<T> pool)
	{
		this.pool = pool;
	}

	/// <summary> 取得されたときの処理 </summary>
	public virtual void OnGetted()
	{
		gameObject.SetActive(true);
		isReleased = false;
	}

	public virtual void OnReleased()
	{
		gameObject.SetActive(false);
		isReleased = true;
	}

	public void Release()
	{
		if (!isReleased && gameObject.activeSelf) {
			pool.Release(this as T);
		}
	}
}
