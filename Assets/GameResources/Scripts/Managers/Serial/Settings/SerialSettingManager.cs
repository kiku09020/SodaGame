using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SerialSettingManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] SerialHandler handler;

    [Header("Logs")]
    [SerializeField] List<SerialSettingLog> logList = new List<SerialSettingLog>();

	[Header("UI")]
    [SerializeField,Tooltip("�|�[�g�I���h���b�v�_�E��")] TMP_Dropdown dropdown;
    [SerializeField,Tooltip("�w�i�摜")] Image backImage;
    [SerializeField, Tooltip("��O�\���摜")] ExceptionDialog excDialog;

    [Header("Paramters")]
    [SerializeField] float backFadeDuration = .5f;

    bool enableBack;
    bool isException;

    //--------------------------------------------------

    void Start()
    {
        backImage.DOFade(0, 0);
        SetSelectLogUI();

        // �ؒf����
        SerialSelector.CheckDisconnected(this.GetCancellationTokenOnDestroy()).Forget();
    }

	//--------------------------------------------------

	private void FixedUpdate()
	{
        // ��O�_�C�A���O�̕\��
        DispSerialException();

        RunLogEvents();

		// ���O�w�i �L����
		if (CheckLogConditions()) {
			if (!enableBack) {
				backImage.gameObject.SetActive(true);
				backImage.DOFade(.5f, backFadeDuration);
				enableBack = true;
			}
		}

        // ���O�w�i ������
        else {
			if (enableBack) {
				backImage.DOFade(0, backFadeDuration)
						 .OnComplete(() => backImage.gameObject.SetActive(false));
				enableBack = false;
			}
		}
	}

    // �h���b�v�_�E���̃I�v�V�����̍X�V
    void SetSelectLogUI()
    {
        dropdown.ClearOptions();
        dropdown.AddOptions(SerialSelector.CheckPortNames());       // �|�[�g�����擾���Ēǉ�
    }

	//--------------------------------------------------

	// ���O�C�x���g�̎��s
	void RunLogEvents()
    {
        for(int i = 0; i < logList.Count; i++) {
            var current = logList[i];
            var targetList = logList;

            // ���݂̃C���X�^���X���O�̗v�f�̏�����false��
            bool enable = logList.GetRange(0, i).TrueForAll(x => !x.SettingCondition.Condition);

            // �L���ł���΁A���s
            if(enable) {
                current.RunEvent();
            }
        }
	}

    // �����������Ă��邩
    bool CheckLogConditions()
    {
        foreach(var log in  logList) {
            if (log.SettingCondition.Condition) {
                return true;
            }
        }

        return false;
    }

	//--------------------------------------------------

    /// <summary> ��O��\������ </summary>
    void DispSerialException()
    {
        if (!isException && handler?.SerialException != null) {
            isException = true;

            excDialog.DispException(handler.SerialException.Message);
        }
    }

    /// <summary> ��O�̊m�F�{�^�� </summary>
    public void ConfirmExcpt()
    {
        handler.SerialException = null;

        isException = false;
        excDialog.UnDispException();
    }

	//--------------------------------------------------
    /* UI Event Methods */

    // �h���b�v�_�E���̃I�v�V�����̍X�V
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
