using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using DG.Tweening;

public class ModelAniController : MonoBehaviour,IVirtualButtonEventHandler
{
	//外部赋值
	public DOTweenAnimation[] childTween; 
	//获取他们父亲物体的旋转script
	public ScaleAndRotate parentSAR;

	GameController gameController;

	void Start()
	{
		VirtualButtonBehaviour[] vb=GetComponentsInChildren<VirtualButtonBehaviour>();
		for(int i=0;i<vb.Length;i++)
		{
			vb [i].RegisterEventHandler (this);
		}

		for (int i = 0; i < childTween.Length; i++) {
			childTween [i].GetComponent<BoxCollider> ().enabled = false;
		}

		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
	}


	public void OnButtonPressed(VirtualButtonAbstractBehaviour vb)
	{
		switch (vb.VirtualButtonName) {
		case "split":
			//..
			vb.GetComponent<Renderer>().material.color=Color.red;
//			Debug.Log("split...press");
			break;
		case "combin":
			//..
//			Debug.Log("combin...press");
			vb.GetComponent<Renderer>().material.color=Color.red;
			break;
		default:
			//..
			break;
		}
	}

	public void OnButtonReleased(VirtualButtonAbstractBehaviour vb)
	{
		switch (vb.VirtualButtonName) 
		{
		case "split":
			//....
			vb.GetComponent<Renderer>().material.color=Color.white;
//			Debug.Log ("split...released");
			ResetParentAndChild ();
			parentSAR.isCollider = true;
			parentSAR.enabled = false;
			parentSAR.GetComponent<CollierController> ().childTag.Clear ();
			for (int i = 0; i < childTween.Length; i++) {
				//这个很重要
				if (childTween [i].GetComponent<ScaleAndRotate> () != null) {
					ScaleAndRotate tarSAR = childTween [i].GetComponent<ScaleAndRotate> ();
					//将子物体缩放旋转归为
					tarSAR.transform.DOScale (new Vector3 (1, 1, 1), 0.1f);
					tarSAR.transform.DOLocalRotate (new Vector3 (0, 0, 0), 0.1f);
				}
			}
			Transform tra = parentSAR.transform;
			//然后将父物体归为
			tra.DOScale (new Vector3 (0.15f, 0.15f, 0.1f), 0.1f);
			tra.DOLocalRotate (new Vector3 (0, 0, 0), 0.1f);
			tra.DOLocalMove (new Vector3 (0, 0.056f, 0), 0.1f);

			for (int i = 0; i < childTween.Length; i++) {
				ScaleAndRotate tarSAR = childTween [i].GetComponent<ScaleAndRotate> ();
				tarSAR.enabled = true;
				tarSAR.isCollider = false;
				tarSAR.isDragMove = false;
				childTween [i].DOPlayForward ();
			}
			StartCoroutine (MoveLater());
			//....
			break;
		case "combin":
			//..
			vb.GetComponent<Renderer>().material.color=Color.white;
			gameController.lastTrans=null;
//			Debug.Log ("combin...released");
			ResetParentAndChild ();
			//...
			break;
		default:
			//..
			break;
		}
	}


	/// <summary>
	/// Resets the position.将父对象和子对象的trasform复原
	/// </summary>
	/// <param name="tra">Tra.</param>
	public void ResetParentAndChild()
	{
		parentSAR.isCollider = false;
		parentSAR.enabled = false;
		gameController.lastTrans = null;
		for (int i = 0; i < childTween.Length; i++) 
		{
			if (childTween [i].GetComponent<ScaleAndRotate> () != null) 
			{
				ScaleAndRotate tarSAR = childTween [i].GetComponent<ScaleAndRotate> ();
				tarSAR.isCollider = true;
				tarSAR.isRoteteSelf = false;
				tarSAR.isDragMove = false;
				//将子物体缩放旋转归为
				tarSAR.transform.DOScale(new Vector3(1,1,1),0.1f);
				tarSAR.transform.DOLocalRotate (new Vector3(0,0,0),0.1f);
			}
			childTween [i].GetComponent<BoxCollider> ().enabled = false;
			childTween [i].DOPlayBackwards ();
			//这个很重要
		}
		Transform tra=parentSAR.transform;
		//然后将父物体归为
		tra.DOScale (new Vector3(0.15f,0.15f,0.1f),0.1f).SetDelay(0.1f);
		tra.DOLocalRotate (new Vector3(0,0,0),0.1f).SetDelay(0.1f);
		tra.DOLocalMove (new Vector3(0,0.056f,0),0.1f).SetDelay(0.2f);
	}

	IEnumerator MoveLater()
	{
		yield return new WaitForSeconds (2);
		for (int i = 0; i < childTween.Length; i++) {
			childTween [i].GetComponent<BoxCollider> ().enabled = true;
			childTween [i].GetComponent<ScaleAndRotate> ().isRoteteSelf = true;
		}
	}
}
