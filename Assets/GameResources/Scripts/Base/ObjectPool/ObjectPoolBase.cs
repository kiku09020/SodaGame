using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Pool
{
	public abstract class ObjectPoolBase<T> : MonoBehaviour where T : PooledObject<T>
	{
		/* Fields */
		[Header("ObjectPool")]
		[SerializeField, Tooltip("二重解放のチェック")] protected bool checkCollection = true;
		[SerializeField, Tooltip("Stackの初期サイズ")] protected int defaultCapacity = 20;
		[SerializeField, Tooltip("プールの最大サイズ")] protected int maxSize = 100;

		//-------------------------------------------------------------------
		/* Events */
		void Awake()
		{
			CreatePool();
		}

		//-------------------------------------------------------------------
		/* Methods */
		/// <summary> プールの作成 </summary>
		protected abstract void CreatePool();

		/// <summary> オブジェクトをプールから取得するときの処理 </summary>
		protected abstract void OnGetFromPool(T obj);

		/// <summary> オブジェクトをプールに返すときの処理 </summary>
		protected abstract void OnReleaseToPool(T obj);

		/// <summary> プール内のオブジェクトを削除するときの処理 </summary>
		protected abstract void OnDestroyObject(T obj);

	}
}