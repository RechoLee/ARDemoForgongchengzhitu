using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Vuforia;
using DG.Tweening;
using UnityEngine.UI;
using System.IO;
using System.Text;

/// <summary>
/// Game controller.游戏控制器，提供全局的访问
/// </summary>
public class GameController : MonoBehaviour	//实现,IVirtualButtonEventHandler
{
	//存储下载资源的一个字典
	public Dictionary<string,GameObject> loadObj=new Dictionary<string, GameObject>();

	//连续按几次退出
	private int backCount=0;

	//扫描的UI对象
	public GameObject scan;

	public Material mat; 

	public Text localText;

	/// <summary>
	/// The image target tag.获取当前识别出来的ImageTarget的tag
	/// </summary>
	[HideInInspector]
	public string imageTargetTag="";

	void Awake()
	{
		//将屏幕设置为横屏显式
		Screen.orientation = ScreenOrientation.LandscapeLeft;
	}

	void Update()
	{
		//连续按两次返回键退出应用
//		if (Input.GetKeyDown (KeyCode.Escape))
//		{
//			backCount++;
//			StartCoroutine (QuitAPP());
//			if (backCount > 1)
//			{
//				Application.Quit();
//			}
//		}

		DrayTar ();
	}

	/// <summary>
	/// Quits the AP.控制应用退出
	/// </summary>
	/// <returns>The AP.</returns>
//	IEnumerator QuitAPP()
//	{
//		yield return new WaitForSeconds (1f);
//		backCount = 0;
//	}
		



//	IEnumerator FocusCamera()
//	{
//		//question
//		CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_TRIGGERAUTO);
//		yield return new WaitForSeconds (1f);
//		CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
//
//	}

//
//	float clickDur=0f;
//	bool IsDoubleClick()
//	{
//		if (Input.touchCount == 1)
//		{
//			Touch clickFiger = Input.GetTouch (0);
//			clickDur += Time.deltaTime;
//			if (clickFiger.phase == TouchPhase.Ended && clickFiger.tapCount == 2 && clickDur < 1f) {
//				clickDur = 0f;
//				return true;
//			}
//			return false;
//		} 
//		else
//		{
//			clickDur = 0f;
//			return false;
//		}
//	}



	public LayerMask myMask;
	private float nextTimer =1.5f;
	private Transform tarTrans = null;
	public Transform lastTrans = null;

	void DrayTar()
	{
		//运行在pc
		if (RuntimePlatform.WindowsEditor == Application.platform) {
			float x = Input.GetAxis ("Mouse X");
			float y = Input.GetAxis ("Mouse Y");
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Input.GetMouseButton (0))
			{
				if (Physics.Raycast (ray, out hit, 1000f, myMask)) 
				{
					if (lastTrans != null) 
					{
						lastTrans.GetComponent<ScaleAndRotate> ().enabled = false;
					}
					tarTrans = hit.transform;
					ScaleAndRotate tarScale= tarTrans.GetComponent<ScaleAndRotate> ();
					if (tarScale == null) 
					{
						tarScale = tarTrans.gameObject.AddComponent<ScaleAndRotate> ();
					}
					tarScale.isRoteteSelf = false;
					tarScale.isDragMove = true;
					tarTrans.rotation = Quaternion.Euler (new Vector3(0,0,0));
				}
				if (tarTrans != null) 
				{
					if (!tarTrans.GetComponent<ScaleAndRotate> ().isCollider) {
//						tarTrans.position += new Vector3 (x, 0f, y) * 15f;
						Vector3 v1=Camera.main.ScreenToViewportPoint(Input.mousePosition);
						Vector3 v2 = Camera.main.WorldToViewportPoint (tarTrans.position);
						tarTrans.position = Camera.main.ViewportToWorldPoint (new Vector3(v1.x,v1.y,v2.z));
					}
				}
			}
			if (Input.GetMouseButtonUp (0)) 
			{
				if (tarTrans != null) {
					tarTrans.GetComponent<ScaleAndRotate> ().isDragMove = false;
					lastTrans = tarTrans;
					lastTrans.GetComponent<ScaleAndRotate> ().enabled = true;
					tarTrans = null;
				}
			}
		}

		//安卓手机上的移动操作，通过移动可以实现两个物体的合并
		if(Application.platform==RuntimePlatform.Android)
		{
			if (Input.touchCount>0)
			{
				Touch figer = Input.GetTouch (0);
				if (figer.phase == TouchPhase.Stationary)
				{
					Ray ray = Camera.main.ScreenPointToRay (figer.position);
					RaycastHit hit;
					if (Physics.Raycast (ray,out hit,1000f,myMask))
					{
						nextTimer -= Time.deltaTime;
						if (nextTimer<0 && tarTrans == null) 
						{
							if (lastTrans != null) {
								lastTrans.GetComponent<ScaleAndRotate> ().enabled = false;
							}
							tarTrans = hit.transform;
							ScaleAndRotate tarScale= tarTrans.GetComponent<ScaleAndRotate> ();
							if (tarScale == null) {
								tarScale = tarTrans.gameObject.AddComponent<ScaleAndRotate> ();
							}
							tarScale.isRoteteSelf = false;
							tarScale.isDragMove = true;
							Handheld.Vibrate ();
							tarTrans.rotation = Quaternion.Euler (new Vector3(0,0,0));
						}
					}
				}

				if (figer.phase == TouchPhase.Ended) 
				{
					nextTimer =1.5f;
					if (tarTrans != null) {
						tarTrans.GetComponent<ScaleAndRotate> ().isDragMove = false;
						lastTrans = tarTrans;
						lastTrans.GetComponent<ScaleAndRotate> ().enabled = true;
						tarTrans = null;
					}
				}

				if (figer.phase == TouchPhase.Moved && tarTrans != null) 
				{
					if (!tarTrans.GetComponent<ScaleAndRotate> ().isCollider) {
//						Vector2 nextDir = figer.deltaPosition;
//						tarTrans.position += new Vector3 (nextDir.x, 0f, nextDir.y) * Time.deltaTime *40f;
						Vector3 v1=Camera.main.ScreenToViewportPoint(figer.position);
						Vector3 v2 = Camera.main.WorldToViewportPoint (tarTrans.position);
						tarTrans.position = Camera.main.ViewportToWorldPoint (new Vector3(v1.x,v1.y,v2.z));
					}
				}
			}
		}
	}

	/// <summary>
	/// Loads the model on server.从服务器端加载模型
	/// </summary>
	/// <returns>The model on server.</returns>
	/// <param name="modelName">Model name.传入的assetBundle包</param>
	public IEnumerator LoadModelOnServer(string modelName)
	{
		//pc上的代码
		if (Application.platform == RuntimePlatform.WindowsEditor) {
//			string url = "http://home.goonls.com/model/pc/" + modelName;
			string url = "http://home.goonls.com/model/pc/" +"7-2";
			WWW www = WWW.LoadFromCacheOrDownload (url, 1);
			yield return www;
			
			if (www.error == null) {
				AssetBundle abModel = www.assetBundle;
				GameObject modelObj = abModel.LoadAsset<GameObject> (modelName + ".prefab");
				//将模型加到数组中
				loadObj.Add (modelName, modelObj);
				InitLoadObj (modelName);
				abModel.Unload (false);
				Debug.Log ("初始化完成");
			} else {
				Debug.Log (www.error);
				//加载错误,相应的提示信息
			}

			www.Dispose ();
		}

		//运行在andriod
		if (Application.platform == RuntimePlatform.Android) {
			Debug.Log ("Comein");
			string url = "http://home.goonls.com/model/andriod/"+"7-2";
			WWW www = WWW.LoadFromCacheOrDownload (url, 1);
			yield return www;

			if (www.error == null)
			{
				AssetBundle abModel = www.assetBundle;
				GameObject modelObj = abModel.LoadAsset<GameObject> (modelName + ".prefab");
				//将模型加到数组中
				loadObj.Add (modelName, modelObj);
				InitLoadObj (modelName);
				abModel.Unload (false);
			} else {
				Debug.Log (www.error);
				//加载错误,相应的提示信息
			}
			www.Dispose ();
		}
	}


	/// <summary>
	/// Inits the load object.初始化网络加载出来的模型
	/// </summary>
	public void InitLoadObj(string name)
	{
		GameObject model;
		if (loadObj.TryGetValue (name, out model)) 
		{
			GameObject parentImageTarget = GameObject.FindGameObjectWithTag (name);
			GameObject tarObj=Instantiate<GameObject>(model,new Vector3(0,-72,0),Quaternion.identity,parentImageTarget.transform);
			tarObj.AddComponent<SetRenderQueue> ();
			tarObj.AddComponent<ResetModel> ();
			tarObj.transform.localScale *= 0.2f;
			Renderer[] renders = tarObj.GetComponentsInChildren<Renderer> ();
			for (int i = 0; i < renders.Length; i++) {
				renders [i].material = mat;
			}
			ScaleAndRotate rotateSpt= tarObj.AddComponent<ScaleAndRotate> ();
			rotateSpt.isDragMove =false;
			rotateSpt.isRoteteSelf =true;
			tarObj.transform.DOLocalMoveY (0.18f,8).SetDelay(2);
			tarObj.layer = LayerMask.NameToLayer ("target");
		}
	}

	public void InitTargetStatus(string name)
	{
		GameObject parentImageTarget = GameObject.FindGameObjectWithTag (name);
		lastTrans = null;
	}


	public void ResetButton()
	{
		if (!string.IsNullOrEmpty (imageTargetTag))
		{
			GameObject parentImageTarget = GameObject.FindGameObjectWithTag (imageTargetTag);
			switch (imageTargetTag) {
			case "7-1":
			//..
				ResetModel reset1 = parentImageTarget.GetComponentInChildren<ResetModel> ();
				reset1.ResetPos (new Vector3(0,0.18f,0));
				reset1.ResetScale (new Vector3(0.2f,0.2f,0.2f));
				break;
			case "13-1":
			//..
				ResetModel reset2 = parentImageTarget.GetComponentInChildren<ResetModel> ();
				reset2.ResetPos (new Vector3 (0, 0.1f, 0));
				reset2.ResetScale (new Vector3(0.2f,0.2f,0.2f));
				break;
			case "p-28":
				//..
				ResetModel reset3 = parentImageTarget.GetComponentInChildren<ResetModel> ();
				reset3.ResetPos (new Vector3 (0, 0.1f, 0));
				reset3.ResetScale (new Vector3(0.2f,0.2f,0.2f));
				break;
			case "8-1":
			//..
				parentImageTarget.GetComponentInChildren<ModelAniController>().ResetParentAndChild();
				break;
			case "group1":
				//..
				parentImageTarget.GetComponentInChildren<ModelAniController>().ResetParentAndChild();
				break;
			default:
				break;
			}
		}
	}

	/// <summary>
	/// Loads the local text.用于读取本地的文字
	/// </summary>
	/// <param name="index">Index.</param>
	public void LoadLocalText(string name)
	{
		string path =Application.dataPath+"/MyText/"+name+".txt";
		StreamReader sr = new StreamReader (path,Encoding.UTF8);
		localText.text=sr.ReadToEnd();

		Debug.Log (path);
	}

}
