// データ用クラス
namespace Extentions.DataManagement {
	// <summary> ゲームの設定データ </summary>
	//[System.Serializable]
	//public class SettingData : GameDataBase
	//{
	//	// 音量ボリューム
	//	public float bgmVolume;
	//	public float seVolume;

	//	// コンストラクタ
	//	public SettingData()
	//	{
	//		bgmVolume = 1.0f;
	//		seVolume = 1.0f;
	//	}
	//}

	/// <summary> ランキングデータ </summary>
	[System.Serializable]
	public class RankingData : GameDataBase {
		public const int RANK_COUNT = 10;
		public int[] rankData = new int[RANK_COUNT];
	}
}