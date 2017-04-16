using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Back scene button.返回到上一个场景
/// </summary>
public class BackSceneBtn : MonoBehaviour
{
	public void BackStartScene()
	{
		SceneManager.LoadScene (0);
	}

	void Update()
	{
		//这里将返回到上一个场景中
		if (Input.GetKeyDown (KeyCode.Escape)) {
			SceneManager.LoadScene (0);
		}
	}
}