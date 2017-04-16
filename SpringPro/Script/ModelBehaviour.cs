using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


/// <summary>
/// Model behaviour.模型自身的一些通用行为
/// </summary>
public class ModelBehaviour : MonoBehaviour
{

	#region 存储模型树节点的一些信息
	//父节点
	public Transform treeParent=null;

	//子节点数组
	public Transform[] treeChildren;

	public SelfInfo myInfo;

	private Transform virtualParent;

	#endregion
	//静止状态
	public bool isStatic=false;
	//初始状态
	public bool isIdle=false;
	//拖动
	public bool isDrag=false;
	//旋转
	public bool isRotate=false;
	//缩放
	public bool  isScale=false;

	//touchRotateSpeed
	public float touchRotateSpeed;

	//mouseRoateSpeed;
	public float mouseRotateSpeed;

	//selfRotateSpeed;
	public float selfRotateSpeed;

	//自定义的agrs
	private TouchEventArgs myTouchArgs;

	//存储物体的本来的颜色
	private Color initColor;

	//拆分的时候的动画
	public Tweener splitTween;

	//合并时候的动画
	public Tweener combineTween;

	//定义一个模型的自旋转轴
	Vector3 rotateSelf=new Vector3(1,1,1);

	//获取自身的一个动画
	public DOTweenAnimation myAni=null;


	void Awake()
	{
		#region 初始化模型树的信息
		myInfo=new SelfInfo(transform);
		myInfo.InitState();
		if (treeParent != null) {
			myInfo.Parent = treeParent;
		}
		if(treeChildren.Length!=0)
		{
			myInfo.Children=treeChildren;
		}
			
		#endregion
	}

	void Start()
	{
		virtualParent=GameObject.FindGameObjectWithTag("VirtualParent").GetComponent<Transform>();

		myTouchArgs = TouchEventController.instance.touchArgs;

		//获取本身材质颜色
		if (GetComponent<Renderer> ()!=null) {
			initColor = GetComponent<Renderer> ().material.color;
		}

		//获取自身的动画片段的脚本
		if (GetComponent<DOTweenAnimation> () != null) {
			myAni = GetComponent<DOTweenAnimation> ();
		}
//		Debug.Log ("position:"+myInfo.Position);
//		Debug.Log ("scale:"+myInfo.Scale);
//		Debug.Log ("rotation:"+myInfo.Rotation);
	}

	void Update()
	{
		//处于初始状态
		if (isIdle) 
		{
			IdleModel ();
		}

		//处于拖放状态
		if (isDrag) 
		{
			DragModel ();
		}

		//处于缩放状态
		if (isScale) 
		{
			ScaleModel ();
		}

		//处于旋转状态
		if (isRotate) 
		{
			RotateModel ();
		}
	
		//test
//		Debug.Log ("position:"+myInfo.Position);
//		Debug.Log ("scale:"+myInfo.Scale);
//		Debug.Log ("rotation:"+myInfo.Rotation);
		//test


	}

	//用来存储触屏的上一次和本次的两手指之间距离
	float lastDistance;
	float nowDistance;
	//缩放模型
	public void ScaleModel()
	{
		//windows
		if (Application.platform == RuntimePlatform.WindowsEditor) 
		{
			if (myTouchArgs.WheelValue > 0) {
				//放大
				transform.localScale*=1.1f;
			}
			if (myTouchArgs.WheelValue < 0) {
				//缩小
				transform.localScale*=0.91f;
			}
			transform.localScale =new Vector3(Mathf.Clamp (transform.localScale.x,0.2f,5),Mathf.Clamp(transform.localScale.y,0.2f,5),Mathf.Clamp(transform.localScale.z,0.2f,5));
		}

		//android
		if (Application.platform == RuntimePlatform.Android) {
			if (myTouchArgs.Figers1.phase == TouchPhase.Began || myTouchArgs.Figers2.phase == TouchPhase.Began) {
				lastDistance = Vector2.Distance (myTouchArgs.Figers1.position,myTouchArgs.Figers2.position);
			}
			if (myTouchArgs.Figers1.phase == TouchPhase.Stationary&& myTouchArgs.Figers2.phase == TouchPhase.Stationary) {
				lastDistance = Vector2.Distance (myTouchArgs.Figers1.position,myTouchArgs.Figers2.position);
			}
			if (myTouchArgs.Figers1.phase == TouchPhase.Moved|| myTouchArgs.Figers2.phase == TouchPhase.Moved) {
				nowDistance=Vector2.Distance (myTouchArgs.Figers1.position,myTouchArgs.Figers2.position);
			}
			if (nowDistance > lastDistance) {
				//放大
				transform.localScale*=1.02f;
			}
			if (nowDistance < lastDistance) {
				//缩小
				transform.localScale*=0.98f;
			}
			transform.localScale =new Vector3(Mathf.Clamp (transform.localScale.x,0.2f,5),Mathf.Clamp(transform.localScale.y,0.2f,5),Mathf.Clamp(transform.localScale.z,0.2f,5));
		}
	}

	//旋转模型
	public void RotateModel()
	{
		//windows
		if (Application.platform == RuntimePlatform.WindowsEditor) {
			transform.Rotate (myTouchArgs.Axis,mouseRotateSpeed*Time.deltaTime,Space.World);
		}
		//android
		if (Application.platform == RuntimePlatform.Android) {
			transform.Rotate (myTouchArgs.Axis,touchRotateSpeed*Time.deltaTime,Space.World);
		}
	}

	//拖动模型
	public void DragModel()
	{
		//windows
		if (Application.platform == RuntimePlatform.WindowsEditor) {
			Vector3 v1=Camera.main.ScreenToViewportPoint(Input.mousePosition);
			Vector3 v2 = Camera.main.WorldToViewportPoint (transform.position);
			transform.position = Camera.main.ViewportToWorldPoint (new Vector3(v1.x,v1.y,v2.z));
		}
		//android
		if (Application.platform == RuntimePlatform.Android) {
			Vector3 v1=Camera.main.ScreenToViewportPoint(myTouchArgs.Figers1.position);
			Vector3 v2 = Camera.main.WorldToViewportPoint (transform.position);
			transform.position = Camera.main.ViewportToWorldPoint (new Vector3(v1.x,v1.y,v2.z));
		}

		if (GetComponent<Renderer> () != null) {
			GetComponent<Renderer> ().material.color = Color.red;
		}
	}

	//初始状态模型
	public void IdleModel()
	{
		//让自身绕着一个轴旋转，处于空闲状态
		transform.Rotate(rotateSelf,selfRotateSpeed*Time.deltaTime,Space.Self);

		//物体材质回到本来颜色
		//获取本身材质颜色
		if (GetComponent<Renderer> ()!=null) {
			GetComponent<Renderer> ().material.color = initColor;
		}
	}

	/// <summary>
	/// Splits the model.拆分模型
	/// </summary>
	/// <param name="splitPos">Split position.拆分到的位置</param>
	public void SplitModel(Vector3 vec)
	{
		splitTween= transform.DOLocalMove(vec,1f);
		splitTween.OnComplete (
			()=>{
				transform.GetComponent<BoxCollider> ().enabled = true;
				AllController.instance.ChangeState (transform, new IdleState ());
				transform.SetParent(virtualParent);
			}
		);
	}

	/// <summary>
	/// Combines the model.按下按钮自动合并
	/// </summary>
	public void CombineModel()
	{
		transform.SetParent (myInfo.Parent);
		GetComponent<BoxCollider>().enabled=false;
		transform.DOScale (myInfo.Scale,0.3f);
		transform.DOLocalRotate (myInfo.Rotation, 0.3f);
		transform.DOLocalMove (myInfo.Position, 0.4f).OnComplete(
			()=>AllController.instance.ChangeState(transform,new StaticState())
		);
	}

	/// <summary>
	/// Combines the model.合并模型
	/// </summary>
	public void DragCombineModel()
	{
		transform.SetParent (myInfo.Parent);
		GetComponent<BoxCollider>().enabled=false;
		transform.DOScale (myInfo.Scale,0.3f);
		transform.DOLocalRotate (myInfo.Rotation, 0.3f);
		transform.DOLocalMove (myInfo.Position, 0.4f);
	}

	//碰撞触发
	void OnTriggerStay(Collider col)
	{
		if (myTouchArgs.TargetTransform == transform) {
			//PC
			if (Application.platform == RuntimePlatform.WindowsEditor) {
				if (col.transform == myInfo.Parent) {
					if (Input.GetMouseButtonUp (0)) {
						TouchEventController.instance.isSplit = true;
						TouchEventController.instance.touchArgs.MyMouseState = MouseState.staticState;
						DragCombineModel();
					}
				}
			}

			//Android
			if (Application.platform == RuntimePlatform.Android) {
				if (col.transform == myInfo.Parent) {
					if (Input.GetTouch (0).phase == TouchPhase.Ended) {
						TouchEventController.instance.isSplit = true;
						TouchEventController.instance.touchArgs.MyMouseState = MouseState.staticState;
						DragCombineModel ();
					}
				}
			}
		}
	}

	/// <summary>
	/// Comes the back init.状态为static，在selfinfo中的初始状态
	/// </summary>
	public void ComeBackInitState()
	{
		transform.localScale = myInfo.Scale;
		transform.localEulerAngles = myInfo.Rotation;
		//将自己状态置为static
		AllController.instance.ChangeState(transform,new StaticState());
	}

}

/// <summary>
/// Self info.自己得一些信息
/// </summary>
public class SelfInfo
{

	private Vector3 position;
	/// <summary>
	/// Gets or sets the position.物体的初始位置
	/// </summary>
	/// <value>The position.</value>
	public Vector3 Position
	{
		get{return position; }
		set{position = value; }
	}

	private Vector3 rotation;
	/// <summary>
	/// Gets or sets the rotation.物体初始的旋转
	/// </summary>
	/// <value>The rotation.</value>
	public Vector3 Rotation
	{
		get{return rotation; }
		set{rotation = value; }
	}

	private Vector3 scale;
	/// <summary>
	/// Gets or sets the scale.物体初始的缩放
	/// </summary>
	/// <value>The scale.</value>
	public Vector3 Scale
	{
		get{return scale; }
		set{scale = value; }
	}

	private Transform self = null;
	/// <summary>
	/// Gets or sets the self.自身的transform
	/// </summary>
	/// <value>The self.</value>
	public Transform Self
	{
		get{return self; }
		set{self = value; }
	}

	private Transform parent=null;
	/// <summary>
	/// Gets or sets the parent.模型的父对象
	/// </summary>
	/// <value>The parent.</value>
	public Transform Parent
	{
		get{return parent; }
		set{parent = value; }
	}

	private Transform[] children;
	/// <summary>
	/// Gets or sets the children.子物体的数组
	/// </summary>
	/// <value>The children.</value>
	public Transform[] Children
	{
		get{return children; }
		set{children = value; }
	}

		
	//无参数构造函数
	public SelfInfo()
	{
		
	}

	public SelfInfo(Transform _self)
	{
		self = _self;
	}

	/// <summary>
	/// Inits the state.初始化自身的位置，缩放，旋转
	/// </summary>
	public void InitState()
	{
		position = self.localPosition;
		scale = self.localScale;
		rotation = self.localEulerAngles;
	}
}

