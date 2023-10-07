using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class BGMManager : AudioManager<BGMManager> 
{
	[SerializeField, Tooltip("一番最初に流すBGM")]
	AudioClip firstBGM;

	[SerializeField,Tooltip("DontDestroyOnLoad")] bool isNotDestroied;

	protected override async void Awake()
	{
		if (isNotDestroied) {
			DontDestroyOnLoad(gameObject);
		}

		base.Awake();

		source.loop = true;

		if(firstBGM != null) {
			await PlayAudio(firstBGM);
		}
	}

	protected override void PlayAudio_Derived(AudioClip clip)
	{
		// クリップを指定して再生する
		source.clip = clip;
		source.Play();
	}

	/// <summary>
	/// フェード付きでBGMを再生する。
	/// </summary>
	/// <param name="bgmName">再生するBGM名</param>
	/// <param name="targetVolume">目標音量</param>
	/// <param name="duration">フェード時間</param>
	/// <param name="resetVolume">音量を最初に0にするか</param>
	/// <param name="isParamReset">パラメータリセットフラグ</param>
	public async void PlayBGMWithFade(string bgmName, float targetVolume = 1, float duration = 1, bool resetVolume = true, bool isParamReset = false)
	{
		// リセットフラグが有効であれば、音量を0にする
		if(resetVolume) {
			source.volume = 0;
		}

		source.DOFade(targetVolume, duration);			// フェード

		await PlayAudio(bgmName, isParamReset);			// BGM再生
	}
}
