using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneControl
{
	/// <summary>
	/// Changes the scene.切换场景
	/// </summary>
	/// <param name="index">Index.场景索引</param>
	public static void ChangeScene(int index)
	{
		SceneManager.LoadScene (index);
	}

	/// <summary>
	/// Changes the scene.切换场景
	/// </summary>
	/// <param name="name">Name.场景名称</param>
	public static void ChangeScene(string name)
	{
		SceneManager.LoadScene (name);
	}
}
