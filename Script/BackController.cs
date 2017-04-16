using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackController : MonoBehaviour
{
	private int backCount=0;

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
	/// Quits the AP.退出程序
	/// </summary>
	/// <returns>The AP.</returns>
	IEnumerator QuitAPP()
	{
		yield return new WaitForSeconds (1f);
		backCount = 0;
	}
}
