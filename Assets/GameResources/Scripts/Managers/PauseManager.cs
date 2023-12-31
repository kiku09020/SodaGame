using GameController.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameController.Manager
{
    public class PauseManager : ObjectComponentBase<GameManager>
    {
        /// <summary> |[Y© </summary>
        public static bool IsPausing { get; private set; }

		//--------------------------------------------------

        public static void Pause()
        {
            IsPausing = true;
            Time.timeScale = 0;     // â~

            // ¹ºêâ~
            AudioManager<BGMManager>.PauseAllAudio();
            AudioManager<SEManager>.PauseAllAudio();

            // PauseUI\¦
            UIManager.ShowUIGroup<PauseUIGroup>();
        }

        public static void UnPause()
        {
            IsPausing = false;
            Time.timeScale = 1;     // â~ð

            // ¹ºÌêâ~ð
            AudioManager<BGMManager>.UnpauseAllAudio();
            AudioManager<SEManager>.UnpauseAllAudio();

            // Q[UIÉß·
            UIManager.ShowLastUIGroup();
        }
    }
}
