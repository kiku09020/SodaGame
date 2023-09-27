using Game.Player;
using GameController.Manager;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GameController {
    public class ScoreManager : MonoBehaviour {
        /* Fields */
        [SerializeField] TextMeshProUGUI scoreText;             // ゲーム中のスコアテキスト
        [SerializeField] TextMeshProUGUI resultScoreText;       // 結果スコアテキスト

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

            // ゲームオーバー時に、結果スコアを適用
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