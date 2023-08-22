using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectComponentBase<T> : MonoBehaviour where T : ObjectCore
{
    [Header("Core")]
    [SerializeField] protected T core;
    [SerializeField] public bool isEnable { get; set; } = true;

    //--------------------------------------------------

	//--------------------------------------------------

	//--------------------------------------------------
}
