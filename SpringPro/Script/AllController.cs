using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// All controller.场景中的主控制器，负责状态的各个物体的状态控制
/// </summary>
public class AllController : MonoBehaviour
{
	public static AllController instance=null;

	/// <summary>
	/// The name of the track.当前识别物体的额名字
	/// </summary>
	[HideInInspector]
	public string trackName="";
	//存放识别的对象
	private List<string> trackNameList = new List<string> ();

	//ui的控制类
	private UIController controllerUI=null;

	private bool dragToggle = false;
	/// <summary>
	/// Gets or sets a value indicating whether this <see cref="AllController"/> drag toggle.设置拖动按钮操作开关
	/// </summary>
	/// <value><c>true</c> if drag toggle; otherwise, <c>false</c>.</value>
	public bool DragToggle
	{
		get{return dragToggle;}
		set{dragToggle = value;}
	}

	//一个存储transform和对应的mgr的字典
	Dictionary<Transform,StateManager> tsDic=new Dictionary<Transform, StateManager>();

	void Awake()
	{
		instance = this;

		Screen.orientation = ScreenOrientation.LandscapeLeft;
	}

	void Start()
	{
		//获取ui控制类
		controllerUI = GameObject.FindGameObjectWithTag ("UIController").GetComponent<UIController> ();
		
		TouchEventController.instance.ModelMouseDele = OnMouse;
		TouchEventController.instance.ModelTouchDele = OnTouch;
	}

	void Update()
	{
		if (!string.IsNullOrEmpty (trackName)) {
			//控制scan的显式
			controllerUI.SetScan (false);
			SetParentCollider (trackName);
			if (!trackNameList.Contains (trackName)) {
				trackNameList.Add (trackName);
				//调用添加状态的方法,传入父物体的名字
				AddAllStateMgr (trackName);
			}
		} else {
			controllerUI.SetScan (true);
		}

		//场景的跳转
		ChangeScene();
	}

	void SetParentCollider(string name)
	{
		Collider col=GameObject.FindGameObjectWithTag (name).GetComponent<Collider> ();
		if (col&&dragToggle != col.enabled) {
			col.enabled = dragToggle;
		}
	}

	public void SetChildrenCollider(bool toggle)
	{
		if (!toggle) {
			if (!string.IsNullOrEmpty (trackName)) {
				SelfInfo info= GameObject.FindGameObjectWithTag (trackName).GetComponent<ModelBehaviour> ().myInfo;
				for (int i = 0; i < info.Children.Length; i++) {
					info.Children [i].GetComponent<Collider> ().enabled = true;
				}
			}
		}
	}
		
	void ChangeScene()
	{
		//这里将返回到上一个场景中
		if (Input.GetKeyDown (KeyCode.Escape)) {
			SceneManager.LoadScene (0);
		}
			
	}


	/// <summary>
	/// Raises the mouse event.检测鼠标的控制
	/// </summary>
	/// <param name="e">E.</param>
	void OnMouse(TouchEventArgs e)
	{
		if (e.TargetTransform != null) {
			switch (e.MyMouseState) {
			case MouseState.Idle:
				//..
				StateManager sMgr1;
				if (tsDic.TryGetValue (e.TargetTransform, out sMgr1)) {
					sMgr1.ChangeState (new IdleState ());
				}
				break;
			case MouseState.Drag:
				//..
				StateManager sMgr2;
				if (tsDic.TryGetValue (e.TargetTransform, out sMgr2)) {
					sMgr2.ChangeState (new  DragState());
				}
				break;
			case MouseState.Rotate:
				//..
				StateManager sMgr3;
				if (tsDic.TryGetValue (e.TargetTransform, out sMgr3)) {
					sMgr3.ChangeState (new  RotateState());
				}
				break;
			case MouseState.Scale:
				//..
				StateManager sMgr4;
				if (tsDic.TryGetValue (e.TargetTransform, out sMgr4)) {
					sMgr4.ChangeState (new ScaleState());
				} 
				break;
			case MouseState.staticState:
				//..
				StateManager sMgr5;
				if (tsDic.TryGetValue (e.TargetTransform, out sMgr5)) {
					sMgr5.ChangeState (new StaticState());
				} 
				break;
			default :
				//..
				break;
			}
		}

	}


	/// <summary>
	/// Raises the touch event.检测触屏的控制
	/// </summary>
	/// <param name="e">E.</param>
	void OnTouch(TouchEventArgs e)
	{
		if (e.TargetTransform != null) {
			switch (e.MyTouchState) {
			case TouchState.Idle:
			//..
				StateManager sMgr1;
				if (tsDic.TryGetValue (e.TargetTransform, out sMgr1)) {
					sMgr1.ChangeState (new IdleState ());
				}
				break;
			case TouchState.Drag:
			//..
				StateManager sMgr2;
				if (tsDic.TryGetValue (e.TargetTransform, out sMgr2)) {
					sMgr2.ChangeState (new DragState ());
				}
				break;
			case TouchState.Rotate:
			//..
				StateManager sMgr3;
				if (tsDic.TryGetValue (e.TargetTransform, out sMgr3)) {
					sMgr3.ChangeState (new RotateState ());
				}
				break;
			case TouchState.Scale:
			//..
				StateManager sMgr4;
				if (tsDic.TryGetValue (e.TargetTransform, out sMgr4)) {
					sMgr4.ChangeState (new ScaleState ());
				}
				break;
			case TouchState.staticState:
				//..
				StateManager sMgr5;
				if (tsDic.TryGetValue (e.TargetTransform, out sMgr5)) {
					sMgr5.ChangeState (new StaticState());
				}
				break;
			default :
			//..
				break;
			}
		}
	}


	/// <summary>
	/// Creates the state of the base.创建一个基本的状态管理器
	/// </summary>
	/// <returns>The base state.</returns>
	/// <param name="tra">Tra.管理的tansform对象</param>
	StateManager CreateBaseStateMgr(Transform tra)
	{
		StateManager stateMgr = new StateManager (tra);

		IdleState idle = new IdleState ();
		ScaleState scale = new ScaleState ();
		RotateState rotate = new RotateState ();
		DragState drag = new DragState ();
		StaticState staticState = new StaticState ();


		stateMgr.Add (idle);
		stateMgr.Add (scale);
		stateMgr.Add (rotate);
		stateMgr.Add (drag);
		stateMgr.Add (staticState);

		return stateMgr;

	}

	/// <summary>
	/// Adds all state mgr.当物体出现的时候初始化父物体的状态mgr和子物体的mgr
	/// </summary>
	/// <param name="name">Name.传入父物体的tag</param>
	void AddAllStateMgr(string tag)
	{
		Transform parent = GameObject.FindGameObjectWithTag (tag).GetComponent<Transform>();
		StateManager parentStateMgr = CreateBaseStateMgr (parent);
		tsDic.Add (parent,parentStateMgr);
		parentStateMgr.ChangeState (new IdleState());
		SelfInfo parentInfo = parent.GetComponent<ModelBehaviour> ().myInfo;
		for (int i = 0; i < parentInfo.Children.Length; i++) {
			StateManager sm = CreateBaseStateMgr (parentInfo.Children[i]);
			tsDic.Add (parentInfo.Children[i],sm);
			sm.ChangeState (new StaticState());
		}
	}


	/// <summary>
	/// Inits the last target.当选中了其他的物体的时候，将上一次选中的物体的状态置为static
	/// </summary>
	/// <returns>The last target.</returns>
	public  IEnumerator InitLastTarget()
	{
		yield return new WaitForSeconds(1);
		StateManager sm;
		if (TouchEventController.instance.touchArgs.LastTransform != null) {
			if (tsDic.TryGetValue (TouchEventController.instance.touchArgs.LastTransform, out sm)) {
				sm.ChangeState (new StaticState());
			}
		}
	}

	/// <summary>
	/// Autos the combine.自动合并，供按钮注册合并事件
	/// </summary>
	public void AutoCombineBtn(string name)
	{
		//将选中的物体状态置为static
		ChangeState(TouchEventController.instance.touchArgs.TargetTransform,new StaticState());
		//将touchEventArgs中tar置为null
		TouchEventController.instance.touchArgs.TargetTransform=null;
		TouchEventController.instance.isSplit = false;
		ModelBehaviour parentModel = GameObject.FindGameObjectWithTag (name).GetComponent<ModelBehaviour> ();
		SelfInfo parentInfo = parentModel.myInfo;
		for (int i = 0; i < parentInfo.Children.Length; i++){
			if (parentInfo.Children [i].GetComponent<ModelBehaviour> () != null) {
				parentInfo.Children [i].GetComponent<ModelBehaviour> ().CombineModel ();
			}
		}
	}
		

	/// <summary>
	/// Autos the split.自动拆分，供按钮注册拆分事件
	/// </summary>
	public void AutoSplitBtn(string name)
	{
		//将touchEventArgs中tar置为null
		TouchEventController.instance.touchArgs.TargetTransform=null;
		TouchEventController.instance.isSplit = true;
		Transform parent=GameObject.FindGameObjectWithTag (name).GetComponent<Transform> ();
		//切换成static状态
		ModelBehaviour parentModel = parent.GetComponent<ModelBehaviour> ();
		//调用对象自身的方法回归到本来状态
		parentModel.ComeBackInitState ();
		SelfInfo parentInfo = parentModel.myInfo;
		float r =3f;
		float delta = 3.14f /3f;
		for (int i = 0; i < parentInfo.Children.Length; i++){
			if (parentInfo.Children [i].GetComponent<ModelBehaviour> () != null) {
				//计算出圆上的点
				Vector3 pos = new Vector3 (r*Mathf.Cos(delta*i),0,r*Mathf.Sin(delta*i));
				parentInfo.Children [i].GetComponent<ModelBehaviour> ().SplitModel (pos);
			}
		}
	}
		

	/// <summary>
	/// Changes the state.供外部访问的changeState方法，用于切换某个物体的状态
	/// </summary>
	/// <param name="tra">Tra.被操作的对象</param>
	/// <param name="bs">Bs.切换的状态</param>
	public void ChangeState(Transform tra,BaseState bs)
	{
		foreach (var item in tsDic) {
			if (item.Key == tra) {
				item.Value.ChangeState (bs);
			}
		}
	}

}
