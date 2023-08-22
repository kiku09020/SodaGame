using NAudio.Wave;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NAudioController : MonoBehaviour
{
    [Header("Device")]
    [SerializeField] List<string> deviceNames = new List<string>();         // �ėp�f�o�C�X�����X�g
    [SerializeField] int targetDeviceNameIndex;                             // �f�o�C�X�����X�g�̎g�p�C���f�b�N�X

    Dictionary<string,int> deviceDict = new Dictionary<string,int>();       // �f�o�C�X�R���N�V����(key:�f�o�C�X��, value:�f�o�C�X�ԍ�)

    [Header("Directory")]
    [SerializeField] string audDirPath= "\\Assets\\Audio";                  // �����f�B���N�g��

    [Header("Audio")]
    static Dictionary<string, AudioUnit> audioDict = new Dictionary<string, AudioUnit>();      // �����R���N�V����

    //--------------------------------------------------
    // �������N���X
    public class AudioUnit
    {
        string audioFileName;      // �t�@�C����
        string audioFilePath;      // �t�@�C���p�X

        WaveOutEvent waveOut = new WaveOutEvent();  // �����f�[�^
        WaveFileReader reader;

        //------------------------------
        // �R���X�g���N�^
        public AudioUnit(string path, int deviceNum)
        {
            try {
                // �t�@�C�����Z�b�g
                audioFilePath = path;
                audioFileName = System.IO.Path.GetFileNameWithoutExtension(path);

				// WaveOut�̏�����
                waveOut.DeviceNumber = deviceNum;
				reader = new WaveFileReader(audioFilePath);
                LoopStream loop = new LoopStream(reader);

				waveOut.Init(loop);
			}

            catch (System.Exception e) {
                Debug.LogWarning(e);
            }
		}

		// �f�X�g���N�^
		~AudioUnit()
		{
			waveOut?.Dispose();
		}

		//------------------------------
        /// <summary> �������Đ����� </summary>
        public void PlayAudio()
        {
            reader.Position = 0;        // �Đ��ʒu���Z�b�g
            waveOut?.Play();

            print($"{audioFileName} was played");
        }
    }

	//--------------------------------------------------
	void Awake()
    {
        var currentDirPath = System.IO.Directory.GetCurrentDirectory();

		SetAudioUnits(currentDirPath + audDirPath, deviceNames[targetDeviceNameIndex]);
	}

	private void Update()
	{
        // �L�[���͂ōĐ�
        if (Input.GetKeyDown(KeyCode.Space)) {
            audioDict["exp_wav"].PlayAudio();
		}
	}

	private void OnDestroy()
	{
        audioDict.Clear();
	}

	//--------------------------------------------------
	/// <summary> �����Đ� </summary>
	static public void Play(string audioName)
    {
        audioDict[audioName].PlayAudio();
    }

	//--------------------------------------------------
	// �w��t�H���_���̉����t�@�C���̃p�X���擾����
	string[] GetAudioFilePaths(string dirPath)
    {
        return System.IO.Directory.GetFiles(dirPath, "*.wav", System.IO.SearchOption.AllDirectories);
    }

    // �����f�[�^�Z�b�g
    void SetAudioUnits(string dirPath, string deviceName)
    {
        // �R���N�V�����̃��Z�b�g
        audioDict.Clear();
        deviceDict.Clear();

        // �f�o�C�X�擾
        deviceDict = GetDeviceNames();

        // �f�o�C�X�Ɋ܂܂�Ă��邩
        if (deviceDict.ContainsKey(deviceName)) {
            // �t�@�C���̉������C���X�^���X���A�R���N�V�����ɒǉ�
            foreach (var filePath in GetAudioFilePaths(dirPath)) {
                var audUnit = new AudioUnit(filePath, deviceDict[deviceName]);
                audioDict.Add(System.IO.Path.GetFileNameWithoutExtension(filePath), audUnit);
            }
        }

        else {
            throw new System.Exception("�w�肳�ꂽ�f�o�C�X�͑��݂��܂���");
        }
    }

	//--------------------------------------------------
	// �f�o�C�X���擾
	Dictionary<string,int> GetDeviceNames()
    {
        var devices = new Dictionary<string, int>();

        for (int i = 0; i < WaveOut.DeviceCount; i++) {
            var capabilities = WaveOut.GetCapabilities(i);

            // �f�o�C�X���̏d������
            if (!devices.ContainsKey(capabilities.ProductName)) {
                devices.Add(capabilities.ProductName, i);

                print(capabilities.ProductName);
            }
        }

        return devices;
    }
}
