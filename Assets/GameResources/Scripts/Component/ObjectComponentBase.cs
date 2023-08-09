using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectComponentBase<T> : MonoBehaviour where T : ObjectCore
{
    [Header("Core")]
    [SerializeField] protected T core;
    [SerializeField] public bool isEnable { get; set; } = true;

    //--------------------------------------------------

    protected void Awake()
    {
        // ƒCƒxƒ“ƒg’Ç‰Á
        if (core != null) {
            core.OnStartEvent += StartEvent;
            core.OnFixedUpdateEvent += FixedUpdateEvent;
            core.OnUpdateEvent += UpdateEvent;
        }
    }

	//--------------------------------------------------
	void StartEvent()
    {
        if(isEnable) {
            OnStart();
        }
    }

	void FixedUpdateEvent()
	{
        if (isEnable) {
            OnFixedUpdate();
        }
	}

    void UpdateEvent()
    {
        if (isEnable) {
            OnUpdate();
        }
    }

	//--------------------------------------------------
	protected virtual void OnStart() { }
    protected virtual void OnUpdate() { }
    protected virtual void OnFixedUpdate() { }
}
