using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
 {
	public GameObject SlidingPanel;
	public GameObject Ad;
	public GameObject Home;
	public GameObject Icon;

	void LateUpdate()
	{
		PlayerPrefs.SetInt ("不是第一次使用", 10);
	}

	void Start()
	{
		if (PlayerPrefs.HasKey ("不是第一次使用")) 
		{
			Home.SetActive (true);
			Icon.SetActive (true);
			SlidingPanel.SetActive (false);
			Ad.SetActive (false);
		}
	}
}
