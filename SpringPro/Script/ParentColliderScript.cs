using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentColliderScript : MonoBehaviour
{

	//设置碰撞体的显示的小球
	public GameObject colliderSphere;

	void Update()
	{
		if (GetComponent<Collider> ()) {
			Collider col = GetComponent<Collider> ();
			if (col.enabled) {
				colliderSphere.SetActive (true);
			} else {
				colliderSphere.SetActive (false);
			}
		}
	}
}
