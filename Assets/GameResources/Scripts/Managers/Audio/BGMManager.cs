using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class BGMManager : AudioManager<BGMManager> {
	[SerializeField, Tooltip("��ԍŏ��ɗ���BGM")]
	AudioClip firstBGM;

	[SerializeField, Tooltip("DontDestroyOnLoad")] bool isNotDestroied;

	static BGMManager instance;

	public static BGMManager Instance => GetInstance();

	static BGMManager GetInstance()
	{
		if (!instance) {
			// �����̃C���X�^���X����
			instance = FindObjectOfType<BGMManager>();

			// ������ΐV�K�쐬
			if (!instance) {
				SetupInstance();
			}
		}

		return instance;
	}

	// �V�K�쐬
	static void SetupInstance()
	{
		var obj = new GameObject();
		obj.name = "BGMManager";

		instance = obj.AddComponent<BGMManager>();
		DontDestroyOnLoad(obj);
	}

	// �C���X�^���X�̏d���폜
	void RemoveDuplicates()
	{
		// �V�[����ɖ�����ΐV�K�쐬
		if (!instance) {
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		// ���ɂ���Ύ��g���폜
		else {
			Debug.LogError($"*{gameObject} was destroyed.");
			Destroy(gameObject);
		}
	}

	protected override async void Awake()
	{
		RemoveDuplicates();

		// �������āA���g�łȂ���΍폜
		if (isNotDestroied) {
			GetInstance();
		}

		base.Awake();

		source.loop = true;

		if (firstBGM != null) {
			await PlayAudio(firstBGM);
		}
	}

	protected override void PlayAudio_Derived(AudioClip clip)
	{
		// �N���b�v���w�肵�čĐ�����
		source.clip = clip;
		source.Play();
	}

	/// <summary>
	/// �t�F�[�h�t����BGM���Đ�����B
	/// </summary>
	/// <param name="bgmName">�Đ�����BGM��</param>
	/// <param name="targetVolume">�ڕW����</param>
	/// <param name="duration">�t�F�[�h����</param>
	/// <param name="resetVolume">���ʂ��ŏ���0�ɂ��邩</param>
	/// <param name="isParamReset">�p�����[�^���Z�b�g�t���O</param>
	public async void PlayBGMWithFade(string bgmName, float targetVolume = 1, float duration = 1, bool resetVolume = true, bool isParamReset = false)
	{
		// ���Z�b�g�t���O���L���ł���΁A���ʂ�0�ɂ���
		if (resetVolume) {
			source.volume = 0;
		}

		source.DOFade(targetVolume, duration);          // �t�F�[�h

		await PlayAudio(bgmName, isParamReset);         // BGM�Đ�
	}
}
