using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using DG.Tweening;

public class FocuingButton : MonoBehaviour
{
	public void OnFocuing()
	{
		StartCoroutine (CameraFocuing());
		this.GetComponent<RectTransform> ().DOScale (new Vector3(1.5f,1.5f,1f),0.5f).OnComplete(EndAni);
	}

	IEnumerator CameraFocuing()
	{
		CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
		yield return new WaitForSeconds (1f);
		CameraDevice.Instance.SetFocusMode (CameraDevice.FocusMode.FOCUS_MODE_NORMAL);
	}

	void EndAni()
	{
		this.GetComponent<RectTransform> ().DOScale (new Vector3 (1f, 1f, 1f), 0.5f);
	}

//	IEnumerator LocalScale()
//	{
//
//	}
}
