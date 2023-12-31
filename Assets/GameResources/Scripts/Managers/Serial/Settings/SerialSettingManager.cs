using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameController.Manager;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SerialSettingManager : MonoBehaviour {
	[Header("Components")]
	[SerializeField] SerialHandler handler;

	[Header("Logs")]
	[SerializeField] List<SerialSettingLog> logList = new List<SerialSettingLog>();

	[Header("UI")]
	[SerializeField, Tooltip("ポート選択ドロップダウン")] TMP_Dropdown dropdown;
	[SerializeField, Tooltip("背景画像")] Image backImage;
	[SerializeField, Tooltip("例外表示画像")] ExceptionDialog excDialog;

	[Header("Paramters")]
	[SerializeField] float backFadeDuration = .5f;

	[Header("isDebug")]
	[SerializeField] bool isDebug = true;

	bool enableBack;
	bool isException;

	//--------------------------------------------------

	void Start()
	{
		backImage.gameObject.SetActive(false);

		if (!isDebug) {
			SetSelectLogUI();

			// 切断判定
			SerialSelector.CheckDisconnected(this.GetCancellationTokenOnDestroy()).Forget();
		}
	}

	//--------------------------------------------------

	private void Update()
	{
		if (!isDebug) {
			// 例外ダイアログの表示
			DispSerialException();

			RunLogEvents();

			// ログ背景 有効化
			if (CheckLogConditions()) {
				if (!enableBack) {
					PauseManager.Pause();               // 一時停止

					// 背景フェードイン
					backImage.gameObject.SetActive(true);
					backImage.DOFade(.5f, backFadeDuration)
						.SetUpdate(true);
					enableBack = true;
				}
			}

			// ログ背景 無効化
			else {
				if (enableBack) {
					PauseManager.UnPause();             // 一時停止解除

					// 背景フェードアウト
					backImage.DOFade(0, backFadeDuration)
							 .OnComplete(() => backImage.gameObject.SetActive(false))
							 .SetUpdate(true);
					enableBack = false;
				}
			}
		}
	}

	// ドロップダウンのオプションの更新
	void SetSelectLogUI()
	{
		dropdown.ClearOptions();
		dropdown.AddOptions(SerialSelector.CheckPortNames());       // ポート名を取得して追加
	}

	//--------------------------------------------------

	// ログイベントの実行
	void RunLogEvents()
	{
		for (int i = 0; i < logList.Count; i++) {
			// 現在のインスタンスより前の要素の条件がfalseか
			bool enable = logList.GetRange(0, i).TrueForAll(x => !x.SettingCondition.Condition);

			// 有効であれば、実行
			if (enable) {
				logList[i].RunEvent();
			}
		}
	}

	// 条件満たしているか
	bool CheckLogConditions()
	{
		foreach (var log in logList) {
			if (log.SettingCondition.Condition) {
				return true;
			}
		}

		return false;
	}

	//--------------------------------------------------

	/// <summary> 例外を表示する </summary>
	void DispSerialException()
	{
		if (!isException && handler?.SerialException != null) {
			isException = true;

			excDialog.DispException(handler.SerialException.Message);
		}
	}

	/// <summary> 例外の確認ボタン </summary>
	public void ConfirmExcpt()
	{
		handler.SerialException = null;

		isException = false;
		excDialog.UnDispException();
	}

	//--------------------------------------------------
	/* UI Event Methods */

	// ドロップダウンのオプションの更新
	public void ReloadDropDownOptions()
	{
		SetSelectLogUI();
	}

	public void SetDeviceName()
	{
		SerialSelector.SetNewPortName(dropdown.options[dropdown.value].text);
	}

	public void ResetDeviceName()
	{
		SerialSelector.ResetTargetName();
	}
}
