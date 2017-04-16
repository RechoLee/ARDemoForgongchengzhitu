using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
	public  Transform tra1;
	public  Transform tra2;
	public Transform tra3;

	List<Transform> listTra=new List<Transform>();

	void Awake()
	{
		listTra.Add (tra1);
		listTra.Add (tra2);
	}

	void Update()
	{
		Debug.Log(listTra.Contains (tra3));
	}


}
