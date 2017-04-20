using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheSceneCrt : MonoBehaviour
{
	private int backCount = 0;

	void Awake()
	{
		//将屏幕设置为竖屏
		Screen.orientation = ScreenOrientation.Portrait;
	}

	/// <summary>
	/// Loads A.加载ar场景
	/// </summary>
	public void LoadAR()
	{
		SceneControl.ChangeScene (1);
	}

	void Update()
	{
		//连续按两次返回键退出应用
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			backCount++;
			StartCoroutine (QuitAPP());
			if (backCount > 1)
			{
				Application.Quit();
			}
		}
	}

	/// <summary>
	/// Quits the AP.控制应用退出
	/// </summary>
	/// <returns>The AP.</returns>

	IEnumerator QuitAPP()
	{
		yield return new WaitForSeconds (1f);
		backCount = 0;
	}
}
