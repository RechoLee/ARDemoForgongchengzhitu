using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Start button.开始AR场景
/// </summary>
public class StartBtn : MonoBehaviour
{
	void Awake()
	{
		Screen.orientation = ScreenOrientation.Portrait;
	}

	public void StartScene()
	{
		SceneManager.LoadScene (1);
	}
}