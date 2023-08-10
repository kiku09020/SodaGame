using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary> 例外ダイアログ </summary>
public class ExceptionDialog : MonoBehaviour {
	/* Fields */
	[Header("UI")]
	[SerializeField] Image dialogImage;
	[SerializeField] TextMeshProUGUI dialogText;

	[Header("Parameters")]
	[SerializeField] float tweenDuration;
	[SerializeField] Ease tweenEase;

	//-------------------------------------------------------------------
	/* Methods */
	private void Awake()
	{
		// イメージ非表示
		dialogImage.rectTransform.DOScale(0, 0);
		dialogImage.gameObject.SetActive(false);
	}

	/// <summary> ダイアログ表示 </summary>
	/// <param name="message">表示メッセージ</param>
	public void DispException(string message)
	{
		dialogText.text = message;      // メッセージ指定

		dialogImage.gameObject.SetActive(true);
		dialogImage.rectTransform.DOScale(1, tweenDuration)
			.SetEase(tweenEase)
			.SetUpdate(true);
	}

	/// <summary> ダイアログ非表示 </summary>
	public void UnDispException()
	{
		dialogImage.rectTransform.DOScale(0, tweenDuration)
			.SetEase(tweenEase)
			.OnComplete(() => dialogImage.gameObject.SetActive(false))
			.SetUpdate(true);
	}
}
