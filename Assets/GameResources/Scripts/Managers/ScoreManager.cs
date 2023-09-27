using Game.Player;
using GameController.Manager;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GameController {
    public class ScoreManager : MonoBehaviour {
        /* Fields */
        [SerializeField] TextMeshProUGUI scoreText;             // �Q�[�����̃X�R�A�e�L�X�g
        [SerializeField] TextMeshProUGUI resultScoreText;       // ���ʃX�R�A�e�L�X�g

        [SerializeField] PlayerCore player;

        int currentScore;
        int resultScore;

        bool resultOnce;

        //-------------------------------------------------------------------
        /* Properties */
        string ScoreText => $"Score:{currentScore}m";

        //-------------------------------------------------------------------
        /* Events */
        void Awake()
        {

        }

        void FixedUpdate()
        {
            currentScore = (int)player.transform.position.y;

            scoreText.text = ScoreText;

            // �Q�[���I�[�o�[���ɁA���ʃX�R�A��K�p
            if (GameManager.IsGameOvered && !resultOnce) {
                resultOnce = true;
                resultScore = currentScore;
                resultScoreText.text = $"{resultScore}m";
            }
        }

        //-------------------------------------------------------------------
        /* Methods */

    }
}