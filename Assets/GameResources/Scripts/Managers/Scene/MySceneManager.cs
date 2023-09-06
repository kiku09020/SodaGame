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
	/// ���݂̃V�[�����ēǂݍ��݂���
	/// </summary>
	public static void ReloadCurrentScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	/// <summary>
	/// ���̃V�[����ǂݍ���
	/// </summary>
	public static void LoadNextScene()
	{
		LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	/// <summary>
	/// �O�̃V�[����ǂݍ���
	/// </summary>
	public static void LoadPrevScene()
	{
		LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
	}

	/// <summary>
	/// �V�[���ԍ����w�肵�ăV�[����ǂݍ���
	/// </summary>
	/// <param name="sceneIndex">�V�[���ԍ�</param>
	public static void LoadScene(int sceneIndex)
	{
		if(CheckSceneIndex(sceneIndex)) {

			SceneManager.LoadScene(sceneIndex);
		}

		else {
			Debug.LogError("�V�[����ǂݍ��߂܂���ł���");
		}
	}

	/// <summary>
	/// �V�[�������w�肵�ăV�[����ǂݍ���
	/// </summary>
	/// <param name="sceneName">�V�[����</param>
	public static void LoadScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}

	// �V�[�����ǂݍ��߂邩�ǂ����̔���
	static bool CheckSceneIndex(int sceneIndex)
	{
		return (0 <= sceneIndex && sceneIndex < SceneManager.sceneCountInBuildSettings);
	}
}
