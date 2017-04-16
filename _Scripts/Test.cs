using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
	public void TestMy()
	{
		Debug.Log ("test");
	}

	void Start()
	{
		GetComponent<ScaleAndRotate> ().enabled = false;
	}
}
