using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

/// <summary>
/// Toggle button.自定义开关类
/// </summary>
public class ToggleBtn: MonoBehaviour
{

	public GameObject on;
	public GameObject off;

	/// <summary>
	/// Lights the control.控制闪光灯的开关方法
	/// </summary>
	/// <param name="toggle">If set to <c>true</c> toggle.</param>
	public void LightControl(bool toggle)
	{
		on.SetActive (toggle);
		off.SetActive (!toggle);
		//控制闪光灯
		CameraDevice.Instance.SetFlashTorchMode(toggle);
	}

	/// <summary>
	/// Drags the toggle.控制开关的toggle
	/// </summary>
	/// <param name="toggle">If set to <c>true</c> toggle.</param>
	public void DragToggle(bool toggle)
	{
		on.SetActive (toggle);
		off.SetActive (!toggle);
	}


}