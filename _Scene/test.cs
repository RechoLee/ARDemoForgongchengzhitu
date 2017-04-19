using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{

	void Start()
	{
		Debug.Log ("2.5:"+Mathf.Round(2.5f));
		Debug.Log ("3.5:"+Mathf.Round(3.5f));
		Debug.Log ("2.4:"+Mathf.Round(2.4f));
		Debug.Log ("2.6:"+Mathf.Round(2.6f));
		Debug.Log ("-3.5:"+Mathf.Round(-3.5f));
		Debug.Log ("-2.5:"+Mathf.Round(-2.5f));
	}

}
