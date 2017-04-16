using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class LightToggle : MonoBehaviour
{

	public GameObject onLight;
	public GameObject offLight;

	/// <summary>
	/// Lights the control.控制闪光灯的开关方法
	/// </summary>
	/// <param name="toggle">If set to <c>true</c> toggle.</param>
	public void LightControl(bool toggle)
	{
		onLight.SetActive (toggle);
		offLight.SetActive (!toggle);
		//控制闪光灯
		CameraDevice.Instance.SetFlashTorchMode(toggle);
	}
}