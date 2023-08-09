using UnityEngine;
using System.IO.Ports;
using System.Threading;
using System;
using Cysharp.Threading.Tasks;

/// <summary> データを送受信する、シリアル通信クラス </summary>
public class SerialHandler : MonoBehaviour {
	[Header("Serial Options")]
	[SerializeField, Tooltip("開かれるポートのボーレート")] int baudRate = 9600;

	SerialPort serialPort;                  // シリアルポート

	// thread
	Thread readingThread;                   // 読み取り用のスレッド
	bool isThreadRunning;                   // スレッド実行中フラグ

	// message
	string message;
	bool isNewMessageReceived;              // 新しくメッセージを受け取ったかどうか

	/// <summary> ポートが有効か </summary>
	public bool IsPortEnable => serialPort != null;

	public Exception SerialException { get; set; }

	//--------------------------------------------------
	/* Events */
	/// <summary> シリアル通信で、データを受け取った時のイベント </summary>
	public event Action OnDataReceived;

	// 開始時にポートを開く
	async void Awake()
	{
		SerialSelector.OnReseted += Close;
		SerialSelector.OnSetNewPort += Open;

		// ポート選択されるまで待機
		await UniTask.WaitUntil(() => (SerialSelector.TargetPortName != null), cancellationToken: this.GetCancellationTokenOnDestroy());

		Open();
	}

	void Update()
	{
		// 受け取ったら、受け取り時の処理を実行
		if (isNewMessageReceived) {
			OnDataReceived?.Invoke();
		}

		isNewMessageReceived = false;
	}

	// 終了時にポートを閉じる
	private void OnDestroy()
	{
		Close();
	}

	//--------------------------------------------------
	/* OpenClose */

	// シリアルポートを開く
	void Open()
	{
		if (SerialSelector.TargetPortName != null) {
			serialPort = new SerialPort(SerialSelector.TargetPortName, baudRate, Parity.None, 8, StopBits.One);        // ポートインスタンス作成

			try {
				serialPort.Open();                                      // 作成したポートを開く
			}

			catch (Exception e) {
				SerialException = e;
				Debug.LogException(e);

				Close();
			}

			isThreadRunning = true;                                 // 実行中フラグ立てる

			readingThread = new Thread(Read);                       // スレッド作成
			readingThread.Start();                                  // スレッド開始(読み込み)

			print($"{serialPort.PortName} was setuped.");
		}
	}

	// シリアルポートを閉じる
	void Close()
	{
		// フラグ降ろす
		isNewMessageReceived = false;
		isThreadRunning = false;

		if (readingThread != null && readingThread.IsAlive) {
			// スレッドが終了するまで待機
			readingThread.Join();

			print("Thread was joined.");
		}

		// ポートが開いていたら、閉じる
		if (IsPortEnable && serialPort.IsOpen) {
			serialPort.Close();         // 閉じる
			serialPort.Dispose();       // リソース開放

			print($"{serialPort.PortName} was closed.");
		}

		else {
			serialPort?.Close();
			serialPort?.Dispose();
			Debug.LogWarning($"正常に終了しませんでした: {serialPort?.PortName}");
		}
	}

	//--------------------------------------------------
	/* ReadWrite */

	// シリアルポートに読み込む
	void Read()
	{
		while (isThreadRunning && IsPortEnable && serialPort.IsOpen) {
			try {
				// ポートからのバイト数が0より多かったら、読み込み
				if (serialPort.BytesToRead > 0) {
					message = serialPort.ReadLine();            // データ読み込み
					isNewMessageReceived = true;                // メッセージ受け取りフラグ立てる
				}
			}

			// 例外
			catch (Exception exception) {
				Debug.LogWarning(exception.Message);
				message = null;

				if (serialPort != null) {
					serialPort.Close();
					serialPort.Dispose();

					SerialSelector.ResetTargetName();
				}
			}
		}
	}

	/// <summary> コンマで区切られたメッセージを返す </summary>
	public string[] GetSplitedData()
	{
		// 受け取ったメッセージを区切る
		return message.Split(",");
	}

	/// <summary> シリアルポートに書き込む </summary>
	/// <param name="message">書き込む文字列</param>
	public void Write(string message)
	{
		// 書き込み
		try {
			serialPort.Write(message);
		}

		catch (Exception exception) {
			Debug.LogWarning(exception.Message);
		}
	}
}
