using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class CaptureScreen : MonoBehaviour
{
	public void CaptureShot()
	{
		string name = Guid.NewGuid ().ToString ();
		if (Application.platform == RuntimePlatform.WindowsEditor) {
			
			Application.CaptureScreenshot (name + "Model.PNG");

			Debug.Log (Application.dataPath);
			Debug.Log (Application.persistentDataPath);
//			string picPath = Application.persistentDataPath+"/"+name+"Model.PNG";
//
//			File.Move (picPath,"F:\\");
		}
		else if (Application.platform == RuntimePlatform.Android) {
			string destination = "/sdcard/DCIM/Camera/ARPic";
			if (!Directory.Exists(destination))
			{
				Directory.CreateDirectory(destination);
			}
			string mobliepath= destination + "/" + name;
			Application.CaptureScreenshot (name);

			string picPath = Application.dataPath+name;

			File.Move (picPath,"F:\\");
		}
	}
}
