using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager {
	/* Fields */

	//-------------------------------------------------------------------
	/* Properties */

	//-------------------------------------------------------------------
	/* Events */

	//-------------------------------------------------------------------
	/* Methods */

	/// <summary>
	/// 現在のシーンを再読み込みする
	/// </summary>
	public static void ReloadCurrentScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	/// <summary>
	/// 次のシーンを読み込む
	/// </summary>
	public static void LoadNextScene()
	{
		LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	/// <summary>
	/// 前のシーンを読み込む
	/// </summary>
	public static void LoadPrevScene()
	{
		LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
	}

	/// <summary>
	/// シーン番号を指定してシーンを読み込む
	/// </summary>
	/// <param name="sceneIndex">シーン番号</param>
	public static void LoadScene(int sceneIndex)
	{
		if(CheckSceneIndex(sceneIndex)) {

			SceneManager.LoadScene(sceneIndex);
		}

		else {
			Debug.LogError("シーンを読み込めませんでした");
		}
	}

	/// <summary>
	/// シーン名を指定してシーンを読み込む
	/// </summary>
	/// <param name="sceneName">シーン名</param>
	public static void LoadScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}

	// シーンが読み込めるかどうかの判定
	static bool CheckSceneIndex(int sceneIndex)
	{
		return (0 <= sceneIndex && sceneIndex < SceneManager.sceneCountInBuildSettings);
	}
}
