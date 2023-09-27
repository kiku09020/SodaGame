using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Base.Pool
{
	public interface IPooledObject<T> where T : PooledObject<T>
	{
		bool IsReleased { get; }

		/// <summary> 作成されるときの処理 </summary>
		void OnCreated(IObjectPool<T> pool);

		/// <summary> 取得されるときの処理 </summary>
		void OnGetted();

		/// <summary> プールに返されるときの処理 </summary>
		void OnReleased();
	}

	public class PooledObject<T> : ObjectCore, IPooledObject<T> where T : PooledObject<T>
	{
		protected IObjectPool<T> pool;      // 格納されるオブジェクトプール

		public bool IsReleased { get; protected set; }

		//--------------------------------------------------
		public virtual void OnCreated(IObjectPool<T> pool)
		{
			this.pool = pool;
		}

		public virtual void OnGetted()
		{
			gameObject.SetActive(true);
			IsReleased = false;
		}

		public virtual void OnReleased()
		{
			gameObject.SetActive(false);
			IsReleased = true;
		}

		public void Release()
		{
			if (!IsReleased) {
				pool.Release(this as T);
			}
		}
	}
}