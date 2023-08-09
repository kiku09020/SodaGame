using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameController.Manager;

public class PlayerController : MonoBehaviour {

	[Header("Components")]
	[SerializeField] DeviceThreasholds threasholds;

	/* Properties */
	KeyController key;
	DeviceController device;

	/// <summary>  有効なコントローラー </summary>
	public static ControllerBase ActiveController { get; private set; }

	/// <summary> コントローラーがデバイスかどうか </summary>
	public static bool ActiveCtrlIsDevice => (ActiveController is DeviceController);

	//--------------------------------------------------

	[Serializable]
	public class DeviceThreasholds {
		[Header("デバイス")]
		[SerializeField, Tooltip("上傾き")] float upThreashold = .7f;
		[SerializeField, Tooltip("下傾き")] float downThreashold = .1f;
		[SerializeField, Tooltip("横傾き")] float sideThreashold = .5f;

		public float Up => upThreashold;
		public float Down => downThreashold;
		public float Side => sideThreashold;
	}

	//--------------------------------------------------

	public abstract class ControllerBase {
		public abstract bool IsUp { get; }
		public abstract bool IsDown { get; }
		public abstract bool IsLeft { get; }
		public abstract bool IsRight { get; }

		public abstract bool IsPressed { get; }
		//public abstract bool IsReleased { get; }

		public abstract bool IsPressedPauseButton { get; }

		public abstract bool IsAxisX { get; }

		public bool IsInputAnyKey()
		{
			if (IsUp || IsDown || IsLeft || IsRight || IsPressed || IsPressedPauseButton) {
				return true;
			}

			return false;
		}
	}

	/// <summary> キー入力 </summary>
	public class KeyController : ControllerBase {
		public override bool IsUp => Input.GetKey(KeyCode.UpArrow);
		public override bool IsDown => Input.GetKey(KeyCode.DownArrow);
		public override bool IsLeft => Input.GetKey(KeyCode.LeftArrow);
		public override bool IsRight => Input.GetKey(KeyCode.RightArrow);

		public override bool IsPressed => Input.GetKey(KeyCode.Space);
		//public override bool IsReleased => Input.GetKeyUp  (KeyCode.Space);

		public override bool IsPressedPauseButton => Input.GetKey(KeyCode.Escape);

		public override bool IsAxisX => (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1);
	}

	/// <summary> デバイス入力 </summary>
	public class DeviceController : ControllerBase {
		DeviceThreasholds threasholds;

		public DeviceController(DeviceThreasholds threasholds)
		{
			this.threasholds = threasholds;
		}

		public override bool IsUp => (DeviceDataReceiver.Acc.y >= threasholds.Up);
		public override bool IsDown => (DeviceDataReceiver.Acc.y <= threasholds.Down && DeviceDataReceiver.Acc.y != 0);
		public override bool IsLeft => (DeviceDataReceiver.Acc.x >= threasholds.Side);
		public override bool IsRight => (DeviceDataReceiver.Acc.x <= -threasholds.Side);

		public override bool IsPressed => (DeviceDataReceiver.IsPressedA);
		//public override bool IsReleased => (receiver.IsPressed);

		public override bool IsPressedPauseButton => (DeviceDataReceiver.IsPressedB);

		public override bool IsAxisX => (Mathf.Abs(DeviceDataReceiver.Acc.x) >= threasholds.Side);
	}

	//--------------------------------------------------

	private void Start()
	{
		key = new KeyController();
		device = new DeviceController(threasholds);

		ActiveController = key;
	}

	private void Update()
	{
		ChangeActiveDevice();
		CheckPauseButton();
	}

	//--------------------------------------------------
	// デバイスが入力されたら、そのデバイスに切り替え
	void ChangeActiveDevice()
	{
		if (key.IsInputAnyKey()) {
			ActiveController = key;
		}

		else if (device.IsInputAnyKey()) {
			ActiveController = device;
		}
	}

	// ポーズ入力判定
	void CheckPauseButton()
	{
		// ポーズ中じゃない時にポーズボタンが入力されたら、ポーズ
		if (!PauseManager.IsPausing && ActiveController.IsPressedPauseButton) {
			PauseManager.Pause();
		}

		// ポーズ時にポーズボタンを入力したらポーズ解除
		else if (PauseManager.IsPausing && ActiveController.IsPressedPauseButton) {
			PauseManager.UnPause();
		}
	}
}