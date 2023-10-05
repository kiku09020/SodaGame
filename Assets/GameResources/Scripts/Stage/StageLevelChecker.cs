using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage {
	/// <summary> ステージの高さによって、難易度変化させる難易度管理クラス </summary>
	public class StageLevelChecker : MonoBehaviour {
		/* Fields */
		[SerializeField,Tooltip("レベルが上がる高さの間隔")] 
		float levelUpHeightDuration = 100;

		float currentHeight;
		float targetHeight;

		//-------------------------------------------------------------------
		/* Properties */
		/// <summary> 難易度レベル </summary>
		public static int DifficultyLevel { get; private set; }

		//-------------------------------------------------------------------
		/* Events */
		void Awake()
		{
			DifficultyLevel = 0;
			targetHeight += levelUpHeightDuration;
		}

		void FixedUpdate()
		{
			CheckHeight();
		}

		//-------------------------------------------------------------------
		/* Methods */
		// 高さチェック
		void CheckHeight()
		{
			// 高さ取得
			currentHeight =  Camera.main.transform.position.y;

			// 目標の高さよりも上に来れば、レベルアップ
			if (currentHeight >= targetHeight) {
				DifficultyLevel++;
				targetHeight += levelUpHeightDuration;
			}
		}
	}
}