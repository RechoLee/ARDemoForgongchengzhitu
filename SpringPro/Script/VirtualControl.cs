using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using DG.Tweening;

public class VirtualControl : MonoBehaviour,IVirtualButtonEventHandler
{
	public GameObject infoCube;

	Color[] colors=new Color[3];

	private int i=0;

	void Start()
	{
		//注册事件
		VirtualButtonBehaviour[] vb=GetComponentsInChildren<VirtualButtonBehaviour>();
		for(int i=0;i<vb.Length;i++)
		{
			vb [i].RegisterEventHandler (this);
		}

		colors [0] = Color.red;
		colors [1] = Color.green;
		colors [2] = Color.blue;
	}

	#region IVirtualButtonEventHandler implementation
	void IVirtualButtonEventHandler.OnButtonPressed (VirtualButtonAbstractBehaviour vb)
	{
		switch (vb.VirtualButtonName) {
		case "p-28":
			//..

			break;
		case "7-1":
			//..

			break;
		case "group1":
			//..
			Debug.Log ("shake");
			infoCube.transform.DOScale (new Vector3(0.01f,0.01f,0.01f),2).OnComplete(
				()=>{
					infoCube.SetActive(false);
					//调出text面板
					UIController.instance.MoveTextPanel(
						//自己顶一个的一个回掉函数
						()=>{
							infoCube.SetActive(true);
							infoCube.transform.DOScale(new Vector3(0.05f,0.05f,0.05f),2);
						}
					);
				}

			);
			break;
		default :
			break;
		}
	}
	void IVirtualButtonEventHandler.OnButtonReleased (VirtualButtonAbstractBehaviour vb)
	{
		switch (vb.VirtualButtonName) {
		case "p-28":
			//..
			Renderer[] renderers1= infoCube.GetComponentsInChildren<Renderer>();
			for(int j=0;j<renderers1.Length;j++)
			{
				renderers1 [j].material.color = colors [i % 3];
			}
			i++;
			break;
		case "7-1":
			//..

			Renderer[] renderers2= infoCube.GetComponentsInChildren<Renderer>();
			for(int j=0;j<renderers2.Length;j++)
			{
				renderers2 [j].material.color = colors [i % 3];
			}
			i++;
			break;
		case "group1":
			//..
			break;
		default :
			break;
		}
	}
	#endregion

}
