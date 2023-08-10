using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary> ��O�_�C�A���O </summary>
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
		// �C���[�W��\��
		dialogImage.rectTransform.DOScale(0, 0);
		dialogImage.gameObject.SetActive(false);
	}

	/// <summary> �_�C�A���O�\�� </summary>
	/// <param name="message">�\�����b�Z�[�W</param>
	public void DispException(string message)
	{
		dialogText.text = message;      // ���b�Z�[�W�w��

		dialogImage.gameObject.SetActive(true);
		dialogImage.rectTransform.DOScale(1, tweenDuration)
			.SetEase(tweenEase)
			.SetUpdate(true);
	}

	/// <summary> �_�C�A���O��\�� </summary>
	public void UnDispException()
	{
		dialogImage.rectTransform.DOScale(0, tweenDuration)
			.SetEase(tweenEase)
			.OnComplete(() => dialogImage.gameObject.SetActive(false))
			.SetUpdate(true);
	}
}
