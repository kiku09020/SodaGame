using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Pool
{
	public abstract class SingletonObjectPool<T, PoolObj> : ObjectPoolBase<PoolObj> where T : Component where PoolObj : PooledObject<PoolObj>
	{
		public static T Instance { get; private set; }

		private void Awake()
		{
			if (!Instance) {
				Instance = FindObjectOfType<T>();
			}

			else {
				Destroy(gameObject);
			}
		}
	}
}