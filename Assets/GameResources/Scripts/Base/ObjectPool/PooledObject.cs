using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Base.Pool
{
	public interface IPooledObject<T> where T : PooledObject<T>
	{
		bool IsReleased { get; }

		/// <summary> �쐬�����Ƃ��̏��� </summary>
		void OnCreated(IObjectPool<T> pool);

		/// <summary> �擾�����Ƃ��̏��� </summary>
		void OnGetted();

		/// <summary> �v�[���ɕԂ����Ƃ��̏��� </summary>
		void OnReleased();
	}

	public class PooledObject<T> : ObjectCore, IPooledObject<T> where T : PooledObject<T>
	{
		protected IObjectPool<T> pool;      // �i�[�����I�u�W�F�N�g�v�[��

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