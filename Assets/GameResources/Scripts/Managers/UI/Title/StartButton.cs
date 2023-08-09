using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameController.UI.Title {
    public class StartButton : MonoBehaviour {
        /* Fields */

        //-------------------------------------------------------------------
        /* Properties */

        //-------------------------------------------------------------------
        /* Events */

        //-------------------------------------------------------------------
        /* Methods */
        public void OnStart()
        {
            MySceneManager.LoadNextScene();

        }
    }
}