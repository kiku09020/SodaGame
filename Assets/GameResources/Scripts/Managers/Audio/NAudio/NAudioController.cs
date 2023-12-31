using NAudio.Wave;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NAudioController : MonoBehaviour {
	[Header("Device")]
	[SerializeField] List<string> deviceNames = new List<string>();         // 汎用デバイス名リスト
	[SerializeField] int targetDeviceNameIndex;                             // デバイス名リストの使用インデックス

	Dictionary<string, int> deviceDict = new Dictionary<string, int>();       // デバイスコレクション(key:デバイス名, value:デバイス番号)

	[Header("Directory")]
	[SerializeField] string audDirPath = "\\Assets\\Audio";                  // 音声ディレクトリ

	[Header("Audio")]
	static Dictionary<string, AudioUnit> audioDict = new Dictionary<string, AudioUnit>();      // 音声コレクション

	//--------------------------------------------------
	// 音声情報クラス
	public class AudioUnit {
		string audioFileName;      // ファイル名
		string audioFilePath;      // ファイルパス

		WaveOutEvent waveOut = new WaveOutEvent();  // 音声データ
		WaveFileReader reader;

		//------------------------------
		// コンストラクタ
		public AudioUnit(string path, int deviceNum)
		{
			try {
				// ファイル情報セット
				audioFilePath = path;
				audioFileName = System.IO.Path.GetFileNameWithoutExtension(path);

				IWaveProvider waveProvider;

				// WaveOutの初期化
				waveOut.DeviceNumber = deviceNum;
				reader = new WaveFileReader(audioFilePath);

				// 名前にループ文字列がついていたら、ループにする
				if (audioFileName.Contains("_loop")) {
					waveProvider = new LoopStream(reader);
				}

				else {
					waveProvider = reader;
				}

				waveOut.Init(waveProvider);
			}

			catch (System.Exception e) {
				Debug.LogWarning(e);
			}
		}

		// デストラクタ
		~AudioUnit()
		{
			waveOut?.Dispose();
		}

		//------------------------------
		/// <summary> 音声を再生する </summary>
		public void PlayAudio()
		{
			reader.Position = 0;        // 再生位置リセット
			waveOut?.Play();

			print($"{audioFileName} was played");
		}

		public void StopAudio()
		{
			waveOut?.Stop();
		}
	}

	//--------------------------------------------------
	void Awake()
	{
		var currentDirPath = System.IO.Directory.GetCurrentDirectory();

		SetAudioUnits(currentDirPath + audDirPath, deviceNames[targetDeviceNameIndex]);
	}

	private void OnDestroy()
	{
		audioDict.Clear();
	}

	//--------------------------------------------------
	/// <summary> 音声再生 </summary>
	static public void Play(string audioName)
	{
		if (audioDict.Count != 0) {
			audioDict[audioName]?.PlayAudio();
		}
	}

	/// <summary> 音声停止 </summary>
	static public void Stop(string audioName)
	{
		if(audioDict.Count != 0) {
			audioDict[audioName]?.StopAudio();
		}
	}

	//--------------------------------------------------
	// 指定フォルダ内の音声ファイルのパスを取得する
	string[] GetAudioFilePaths(string dirPath)
	{
		return System.IO.Directory.GetFiles(dirPath, "*.wav", System.IO.SearchOption.AllDirectories);
	}

	// 音声データセット
	void SetAudioUnits(string dirPath, string deviceName)
	{
		// コレクションのリセット
		audioDict.Clear();
		deviceDict.Clear();

		// デバイス取得
		deviceDict = GetDeviceNames();

		// デバイスに含まれているか
		if (deviceDict.ContainsKey(deviceName)) {
			// ファイルの音声をインスタンス化、コレクションに追加
			foreach (var filePath in GetAudioFilePaths(dirPath)) {
				var audUnit = new AudioUnit(filePath, deviceDict[deviceName]);
				audioDict.Add(System.IO.Path.GetFileNameWithoutExtension(filePath), audUnit);
			}
		}

		else {
			throw new System.Exception("指定されたデバイスは存在しません");
		}
	}

	//--------------------------------------------------
	// デバイス名取得
	Dictionary<string, int> GetDeviceNames()
	{
		var devices = new Dictionary<string, int>();

		for (int i = 0; i < WaveOut.DeviceCount; i++) {
			var capabilities = WaveOut.GetCapabilities(i);

			// デバイス名の重複判定
			if (!devices.ContainsKey(capabilities.ProductName)) {
				devices.Add(capabilities.ProductName, i);

				print(capabilities.ProductName);
			}
		}

		return devices;
	}
}
