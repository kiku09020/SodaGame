using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDebugger : MonoBehaviour
{
    /* Fields */
   

//-------------------------------------------------------------------
    /* Properties */

//-------------------------------------------------------------------
    /* Events */
    void Awake()
    {
        
    }

	private void Update()
	{
        // ÉäÉçÅ[Éh
        if (Debug.isDebugBuild && Input.GetKeyDown(KeyCode.R)) {
            MySceneManager.ReloadCurrentScene();
        }
	}

	//-------------------------------------------------------------------
	/* Methods */

}
