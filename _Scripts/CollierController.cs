using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CollierController : MonoBehaviour
{
	GameController gameController;

	public List<string> childTag=new List<string>();

	void Start()
	{
		transform.DOLocalMoveY (0.023f,6).SetDelay(1);
		gameController= GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
	}

	void Update()
	{
		if (childTag.Count==4) 
		{
			GetComponent<ScaleAndRotate> ().isCollider = false;
		}
	}

	void OnTriggerStay(Collider cl)
	{
		if (cl != null) 
		{
			if (childTag.Count<4)
			{
				GetComponent<ScaleAndRotate> ().isCollider = true;
				PlayAni (cl);
			}
		}
	}
		

	/// <summary>
	/// Plaies the ani.根据tag创建对应的动画
	/// </summary>
	/// <param name="tarCl">Tar cl.</param>
	void PlayAni(Collider tarCl)
	{
		if (GameObject.FindGameObjectWithTag (tarCl.tag)!=null)
		{
			//将lastTrasn之为null
			gameController.lastTrans = null;
			if (RuntimePlatform.Android == Application.platform)
			{
				if(Input.GetTouch(0).phase==TouchPhase.Stationary)
				{
					if (tarCl.GetComponent<ScaleAndRotate> ()!= null) {
						ScaleAndRotate tarSAR= tarCl.GetComponent<ScaleAndRotate> ();
						if (tarCl.tag == "group1_zhijia_r") {
							//恢复旋转和位置，因为相同，所以设置一个方法
							ResetSR (tarCl.transform);
							tarCl.transform.DOLocalMove (new Vector3 (0.3f, 0.08f, 0), 1);
							tarSAR.isCollider=true;
							tarSAR.isRoteteSelf = false;
							tarCl.enabled = false;
							if (!childTag.Contains (tarCl.tag)) 
							{
								childTag.Add (tarCl.tag);
							}
						}
						if (tarCl.tag == "group1_zhijia_l") {
							//恢复旋转和位置，因为相同，所以设置一个方法
							ResetSR (tarCl.transform);
							tarCl.transform.DOLocalMove (new Vector3 (-0.3f, 0.08f, 0), 1);
							tarSAR.isCollider=true;
							tarSAR.isRoteteSelf = false;
							tarCl.enabled = false;
							if (!childTag.Contains (tarCl.tag)) {
								childTag.Add (tarCl.tag);
							}
						}
						if (tarCl.tag == "group1_zhuanlun") {
							//恢复旋转和位置，因为相同，所以设置一个方法
							ResetSR (tarCl.transform);
							tarCl.transform.DOLocalMove (new Vector3 (0.015f, 0.82f, 0), 1);
							tarSAR.isCollider=true;
							tarSAR.isRoteteSelf = false;
							tarCl.enabled = false;
							if (!childTag.Contains (tarCl.tag)) {
								childTag.Add (tarCl.tag);
							}
						}
						if (tarCl.tag == "group1_dingzi") {
							//恢复旋转和位置，因为相同，所以设置一个方法
							ResetSR (tarCl.transform);
							tarCl.transform.DOLocalMove (new Vector3 (0, 0, 0), 1);
							tarSAR.isCollider=true;
							tarSAR.isRoteteSelf = false;
							tarCl.enabled = false;
							if (!childTag.Contains (tarCl.tag)) {
								childTag.Add (tarCl.tag);
							}
						}
					}
				}
			}
			if (RuntimePlatform.WindowsEditor == Application.platform) {
				if (Input.GetMouseButtonUp (0)) {
					if (tarCl.GetComponent<ScaleAndRotate> () != null) {
						ScaleAndRotate tarSAR = tarCl.GetComponent<ScaleAndRotate> ();
						if (tarCl.tag == "group1_zhijia_r") {
							//恢复旋转和位置，因为相同，所以设置一个方法
							ResetSR (tarCl.transform);
							tarCl.transform.DOLocalMove (new Vector3 (0.3f, 0.08f, 0), 1);
							tarSAR.isCollider=true;
							tarSAR.isRoteteSelf = false;
							tarCl.enabled = false;
							if (!childTag.Contains (tarCl.tag)) {
								childTag.Add (tarCl.tag);
							}
						}
						if (tarCl.tag == "group1_zhijia_l") {
							//恢复旋转和位置，因为相同，所以设置一个方法
							ResetSR (tarCl.transform);
							tarCl.transform.DOLocalMove (new Vector3 (-0.3f, 0.08f, 0), 1);
							tarSAR.isCollider=true;
							tarSAR.isRoteteSelf = false;
							tarCl.enabled = false;
							if (!childTag.Contains (tarCl.tag)) {
								childTag.Add (tarCl.tag);
							}
						}
						if (tarCl.tag == "group1_zhuanlun") {
							//恢复旋转和位置，因为相同，所以设置一个方法
							ResetSR (tarCl.transform);
							tarCl.transform.DOLocalMove (new Vector3 (0.015f, 0.82f, 0), 1);
							tarSAR.isCollider=true;
							tarSAR.isRoteteSelf = false;
							tarCl.enabled = false;
							if (!childTag.Contains (tarCl.tag)) {
								childTag.Add (tarCl.tag);
							}
						}
						if (tarCl.tag == "group1_dingzi") {
							//恢复旋转和位置，因为相同，所以设置一个方法
							ResetSR (tarCl.transform);
							tarCl.transform.DOLocalMove (new Vector3 (0, 0, 0), 1);
							tarSAR.isCollider=true;
							tarSAR.isRoteteSelf = false;
							tarCl.enabled = false;
							if (!childTag.Contains (tarCl.tag)) {
								childTag.Add (tarCl.tag);
							}
						}
					}
				}
			}
		}
	}
		

	void  ResetSR(Transform tra)
	{
		tra.DOScale (new Vector3(1,1,1),0.1f);
		tra.DOLocalRotate (new Vector3(0,0,0),0.1f);
	}

}
