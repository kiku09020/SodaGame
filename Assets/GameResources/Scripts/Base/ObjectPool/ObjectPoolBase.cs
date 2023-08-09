using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolBase<T> : MonoBehaviour where T:PooledObject<T>
{
	[Header("ObjectPool")]
	[SerializeField] protected T prefab;			// ��������I�u�W�F�N�g�̃v���n�u
	[SerializeField] Transform generatedParent;     // ���������eTransform		

	[Space(10)]
	[SerializeField] bool checkCollection = true;
	[SerializeField] int defaultCapacity = 20;
	[SerializeField] int maxSize = 100;

	protected ObjectPool<T> pool;

	/// <summary> �v�[���̎擾 </summary>
	public ObjectPool<T> GetPool() => pool;

	//--------------------------------------------------
	protected void Start()
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

	/// <summary> �I�u�W�F�N�g���v�[������擾����Ƃ��̏��� </summary>
	protected virtual void OnGetFromPool(T obj)
	{
		obj.OnGetted();
	}


	/// <summary> �I�u�W�F�N�g���v�[���ɕԂ��Ƃ��̏��� </summary>
	protected virtual void OnReleaseToPool(T obj)
	{
		obj.OnReleased();
	}

	/// <summary> �v�[�����̃I�u�W�F�N�g���폜����Ƃ��̏��� </summary>
	protected virtual void OnDestroyObject(T obj)
	{
		Destroy(obj.gameObject);
	}
}
