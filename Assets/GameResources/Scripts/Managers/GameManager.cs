using GameController.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameController.Manager {
    public class GameManager : ObjectCore {
        

        /* Properties */
        /// <summary> �Q�[���N���A�t���O </summary>
        public static bool IsGameCleared { get; private set; }
        /// <summary> �Q�[���I�[�o�[�t���O </summary>
        public static bool IsGameOvered { get; private set; }

        //--------------------------------------------------

        void Start()
        {
            PauseManager.UnPause();

            IsGameCleared = false;
            IsGameOvered = false;
        }

		private void FixedUpdate()
		{
			if (IsGameOvered) {
                UIManager.ShowUIGroup<GameOverUIGroup>();
            }
		}

		//--------------------------------------------------
		/* Setters */
		/// <summary> �Q�[���N���A </summary>
		public static void SetGameCleared() { IsGameCleared = true; }
        /// <summary> �Q�[���I�[�o�[ </summary>
        public static void SetGameOvered() { IsGameOvered = true; }
    }
}