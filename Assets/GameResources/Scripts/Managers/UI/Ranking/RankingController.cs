using Extentions.DataManagement;
using GameController;
using GameController.UI.Title;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RankingController : MonoBehaviour, IDataUserBase<RankingData> {
	/* Fields */

	[Header("Prefab")]
	[SerializeField] RankingUnit unitPrefab;
	[SerializeField] Transform parent;

	[Header("UI")]
	[SerializeField] Color[] rankColors = new Color[RankingData.RANK_COUNT];

	[Header("Data")]
	[SerializeField] RankingDataManager dataManager;

	List<int> rankData = new List<int>(RankingData.RANK_COUNT);
	List<RankingUnit> rankingUnits = new List<RankingUnit>();

	//-------------------------------------------------------------------
	/* Properties */

	//-------------------------------------------------------------------
	/* Events */
	public void SetData(ref RankingData data)
	{
		data.rankData = rankData.ToArray();
	}

	public void GetData(RankingData data)
	{
		rankData = data.rankData.ToList();
	}

	void Awake()
	{
		gameObject.SetActive(false);

		dataManager.OnDataLoaded += GetData;
		dataManager.OnDataSaved += SetData;

		ScoreManager.OnResult -= AddToRanking;
		ScoreManager.OnResult += AddToRanking;

		dataManager.LoadData();

		// インスタンス化、リストに追加
		for (int i = 0; i < RankingData.RANK_COUNT; i++) {
			var unitObj = Instantiate(unitPrefab, parent);
			rankingUnits.Add(unitObj);
		}

		SetRankColors();
		SetRanking();
	}

	//-------------------------------------------------------------------
	/* Methods */
	// ランク画像の指定
	void SetRankColors()
	{
		if (rankColors.Length < 3) return;

		for (int i = 0; i < rankColors.Length; i++) {
			rankingUnits[i].SetRankImageColor(rankColors[i]);
		}
	}

	// ランキングに追加
	void AddToRanking(int score)
	{
		// 要素追加
		rankData.Add(score);

		// ソート後の末尾の要素を削除
		rankData.Sort();
		rankData.Reverse();
		rankData.RemoveAt(rankData.Count - 1);

		dataManager.SaveData();
		SetRanking();
	}

	void SetRanking()
	{
		// テキスト変更
		for (int i = 0; i < RankingData.RANK_COUNT; i++) {
			rankingUnits[i].SetRankText(i + 1);
			rankingUnits[i].SetScoreText(rankData[i]);
		}
	}
}
