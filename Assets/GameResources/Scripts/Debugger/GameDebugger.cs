using Cinemachine;
using Game.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDebugger : MonoBehaviour
{
    /* Fields */
    [SerializeField] PlayerRendererController rendererController;

//-------------------------------------------------------------------
    /* Properties */

//-------------------------------------------------------------------
    /* Events */
    void Awake()
    {
        
    }

	private void Update()
	{
        // �f�o�b�O�r���h�L�����̂�
        if (!Debug.isDebugBuild) return;

        // �����[�h
        if (Input.GetKeyDown(KeyCode.R)) {
            MySceneManager.ReloadCurrentScene();
        }

        if (Input.GetKeyDown(KeyCode.Delete)) {
            rendererController.GameOverPorcess();
        }

        if (Input.GetKeyDown(KeyCode.F12)) {
            PlayerPrefs.SetInt("HighScore", 0);
            print("HighScore was Reseted");
        }
	}

	//-------------------------------------------------------------------
	/* Methods */

}
