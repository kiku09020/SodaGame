// �f�[�^�p�N���X
namespace Extentions.DataManagement {
	// <summary> �Q�[���̐ݒ�f�[�^ </summary>
	//[System.Serializable]
	//public class SettingData : GameDataBase
	//{
	//	// ���ʃ{�����[��
	//	public float bgmVolume;
	//	public float seVolume;

	//	// �R���X�g���N�^
	//	public SettingData()
	//	{
	//		bgmVolume = 1.0f;
	//		seVolume = 1.0f;
	//	}
	//}

	/// <summary> �����L���O�f�[�^ </summary>
	[System.Serializable]
	public class RankingData : GameDataBase {
		public const int RANK_COUNT = 10;
		public int[] rankData = new int[RANK_COUNT];
	}
}