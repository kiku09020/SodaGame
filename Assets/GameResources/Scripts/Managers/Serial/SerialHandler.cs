using UnityEngine;
using System.IO.Ports;
using System.Threading;
using System;
using Cysharp.Threading.Tasks;

/// <summary> �f�[�^�𑗎�M����A�V���A���ʐM�N���X </summary>
public class SerialHandler : MonoBehaviour {
	[Header("Serial Options")]
	[SerializeField, Tooltip("�J�����|�[�g�̃{�[���[�g")] int baudRate = 9600;

	SerialPort serialPort;                  // �V���A���|�[�g

	// thread
	Thread readingThread;                   // �ǂݎ��p�̃X���b�h
	bool isThreadRunning;                   // �X���b�h���s���t���O

	// message
	string message;
	bool isNewMessageReceived;              // �V�������b�Z�[�W���󂯎�������ǂ���

	/// <summary> �|�[�g���L���� </summary>
	public bool IsPortEnable => serialPort != null;

	public Exception SerialException { get; set; }

	//--------------------------------------------------
	/* Events */
	/// <summary> �V���A���ʐM�ŁA�f�[�^���󂯎�������̃C�x���g </summary>
	public event Action OnDataReceived;

	// �J�n���Ƀ|�[�g���J��
	async void Awake()
	{
		SerialSelector.OnReseted += Close;
		SerialSelector.OnSetNewPort += Open;

		// �|�[�g�I�������܂őҋ@
		await UniTask.WaitUntil(() => (SerialSelector.TargetPortName != null), cancellationToken: this.GetCancellationTokenOnDestroy());

		Open();
	}

	void Update()
	{
		// �󂯎������A�󂯎�莞�̏��������s
		if (isNewMessageReceived) {
			OnDataReceived?.Invoke();
		}

		isNewMessageReceived = false;
	}

	// �I�����Ƀ|�[�g�����
	private void OnDestroy()
	{
		Close();
	}

	//--------------------------------------------------
	/* OpenClose */

	// �V���A���|�[�g���J��
	void Open()
	{
		if (SerialSelector.TargetPortName != null) {
			serialPort = new SerialPort(SerialSelector.TargetPortName, baudRate, Parity.None, 8, StopBits.One);        // �|�[�g�C���X�^���X�쐬

			try {
				serialPort.Open();                                      // �쐬�����|�[�g���J��
			}

			catch (Exception e) {
				SerialException = e;
				Debug.LogException(e);

				Close();
			}

			isThreadRunning = true;                                 // ���s���t���O���Ă�

			readingThread = new Thread(Read);                       // �X���b�h�쐬
			readingThread.Start();                                  // �X���b�h�J�n(�ǂݍ���)

			print($"{serialPort.PortName} was setuped.");
		}
	}

	// �V���A���|�[�g�����
	void Close()
	{
		// �t���O�~�낷
		isNewMessageReceived = false;
		isThreadRunning = false;

		if (readingThread != null && readingThread.IsAlive) {
			// �X���b�h���I������܂őҋ@
			readingThread.Join();

			print("Thread was joined.");
		}

		// �|�[�g���J���Ă�����A����
		if (IsPortEnable && serialPort.IsOpen) {
			serialPort.Close();         // ����
			serialPort.Dispose();       // ���\�[�X�J��

			print($"{serialPort.PortName} was closed.");
		}

		else {
			serialPort?.Close();
			serialPort?.Dispose();
			Debug.LogWarning($"����ɏI�����܂���ł���: {serialPort?.PortName}");
		}
	}

	//--------------------------------------------------
	/* ReadWrite */

	// �V���A���|�[�g�ɓǂݍ���
	void Read()
	{
		while (isThreadRunning && IsPortEnable && serialPort.IsOpen) {
			try {
				// �|�[�g����̃o�C�g����0��葽��������A�ǂݍ���
				if (serialPort.BytesToRead > 0) {
					message = serialPort.ReadLine();            // �f�[�^�ǂݍ���
					isNewMessageReceived = true;                // ���b�Z�[�W�󂯎��t���O���Ă�
				}
			}

			// ��O
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

	/// <summary> �R���}�ŋ�؂�ꂽ���b�Z�[�W��Ԃ� </summary>
	public string[] GetSplitedData()
	{
		// �󂯎�������b�Z�[�W����؂�
		return message.Split(",");
	}

	/// <summary> �V���A���|�[�g�ɏ������� </summary>
	/// <param name="message">�������ޕ�����</param>
	public void Write(string message)
	{
		// ��������
		try {
			serialPort.Write(message);
		}

		catch (Exception exception) {
			Debug.LogWarning(exception.Message);
		}
	}
}
