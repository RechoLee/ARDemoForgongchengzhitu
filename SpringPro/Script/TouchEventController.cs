using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Touch event controller.用于对用户手势的封装
/// </summary>
public class TouchEventController : MonoBehaviour
{
	/// <summary>
	/// The instance.静态访问入口
	/// </summary>
	public static TouchEventController instance=null;


	//定义射线可以打到的层
	public LayerMask myMask;
	//用于判断是否在碰撞过程中
	public bool isSplit=false;

	//定义一个计时器变量
	private float timer=1.1f;


	#region 定义委托,用于touch的

	/// <summary>
	/// Scale delegate.触屏事件委托
	/// </summary>
	public delegate void TouchDelegate (TouchEventArgs e);

	#endregion

	#region 定义委托,用于Mouse的

	/// <summary>
	/// Scale delegate.鼠标事件委托
	/// </summary>
	public delegate void MouseDelegate (TouchEventArgs e);

	#endregion


	#region 声明一个委托对象
	/// <summary>
	/// The model scale dele.委托对象
	/// </summary>
	public TouchDelegate ModelTouchDele=null;
	#endregion

	#region 声明一个委托对象
	/// <summary>
	/// The model scale dele.委托对象
	/// </summary>
	public MouseDelegate ModelMouseDele=null;
	#endregion

	#region 定义手指的touchArgs参数
	//定义一个事件的参数，用于返回touch的一些状态
	public TouchEventArgs touchArgs;

	#endregion

	void Start()
	{
		
	}

	void Awake()
	{


		instance = this;

		//实例化touchevent参数,然后将参数的引用传进去
		touchArgs = new TouchEventArgs();
	}

	void Update()
	{

		//安卓上的代码
		if (Application.platform == RuntimePlatform.Android) {

			//更新touch状态
			ChangedTouchState();

			//判断委托是否为空
			if (ModelTouchDele != null) {
				ModelTouchDele (touchArgs);
			}

		}


		//Windows上的代码
		if (Application.platform == RuntimePlatform.WindowsEditor) {

			//更新鼠标状态
			ChangeMouseState();

			//更新鼠标事件参数

			if (ModelMouseDele != null) {
				ModelMouseDele (touchArgs);
			}
		}
	}
		
	/// <summary>
	/// Changeds the state of the touch.判断此时刻touch的状态
	/// </summary>
	void ChangedTouchState()
	{
		//没有手指触屏的时候
		if (Input.touchCount == 0)
		{
//			touchArgs.MyTouchState = TouchState.Idle;
		}
		//有手指触屏时候
		if (Input.touchCount > 0) 
		{
			//一只手指触屏
			if (Input.touchCount == 1) 
			{
				//接着判断为drag
				touchArgs.Figers1=Input.GetTouch(0);

				//旋转的轴
				touchArgs.Axis = Vector3.Cross (new Vector3(touchArgs.Figers1.deltaPosition.x,touchArgs.Figers1.deltaPosition.y,0),Camera.main.transform.forward);

				//创建一条射线
				Ray touchRay = Camera.main.ScreenPointToRay (touchArgs.Figers1.position);
				RaycastHit touchHit;
				if (touchArgs.Figers1.phase== TouchPhase.Stationary) {
					if (Physics.Raycast (touchRay, out touchHit, 2000f, myMask)) {
						timer -= Time.deltaTime;
						if (timer < 0) {
							if (touchArgs.TargetTransform != touchHit.transform) {
								touchArgs.LastTransform = touchArgs.TargetTransform;
								if (!isSplit) {
									StartCoroutine (AllController.instance.InitLastTarget ());
								}
								touchArgs.TargetTransform = touchHit.transform;
							}
							touchArgs.MyTouchState = TouchState.Drag;
							isSplit = false;
						}
					}
				}

				if (touchArgs.Figers1.phase== TouchPhase.Ended) {
					timer = 1.1f;
					if (!isSplit) {
						touchArgs.MyTouchState = TouchState.Rotate;
					}
				}


			}

			//两只手指
			if (Input.touchCount == 2) 
			{
				touchArgs.Figers1= Input.GetTouch (0);
				touchArgs.Figers2 = Input.GetTouch (1);
				if (touchArgs.TargetTransform!= null) {
					//值为缩放状态
					timer = 1.1f;
					if (!isSplit) {
						touchArgs.MyTouchState = TouchState.Scale;
					}
				}
			}

		}
	}


	/// <summary>
	/// Changes the state of the mouse.判断此刻鼠标的状态
	/// </summary>
	void ChangeMouseState()
	{
		if (Input.GetMouseButton (0)) {
			Ray mouseRay = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit mouseHit;
			if (Physics.Raycast (mouseRay, out mouseHit, 2000f, myMask)) {
				timer -= Time.deltaTime;
				if (timer < 0) {
					if (touchArgs.TargetTransform != mouseHit.transform) {
						touchArgs.LastTransform = touchArgs.TargetTransform;
						if (!isSplit) {
							StartCoroutine (AllController.instance.InitLastTarget ());
						}
						touchArgs.TargetTransform = mouseHit.transform;
					}
					touchArgs.MyMouseState = MouseState.Drag;
					isSplit = false;
					return;
				}
			}
		}

		if (touchArgs.TargetTransform != null) {
			if (!Input.GetMouseButton (0)) {
				touchArgs.WheelValue = Input.GetAxis ("Mouse ScrollWheel");
				timer = 1.1f;
				if (Input.GetAxis ("Mouse ScrollWheel") != 0) {
					if (!isSplit) {
						touchArgs.MyMouseState = MouseState.Scale;
					}
				} else {
					if (!isSplit) {
						touchArgs.MyMouseState = MouseState.Rotate;
					}
					touchArgs.Axis = Vector3.Cross (new Vector3 (Input.GetAxis ("Mouse X"), Input.GetAxis ("Mouse Y"), 0), Camera.main.transform.forward);
				}
			}
		}
	}
}

/// <summary>
/// Touch event arguments.传递给
/// </summary>
public class TouchEventArgs:EventArgs
{
	//
	private float wheelValue;
	/// <summary>
	/// Gets or sets the wheel value.鼠标滚轮的参数
	/// </summary>
	/// <value>The wheel value.</value>
	public float WheelValue{
		set{wheelValue = value; }
		get{return wheelValue; }
	}

	private Vector3 axis;
	/// <summary>
	/// Gets or sets the axis.旋转轴的设置
	/// </summary>
	/// <value>The axis.</value>
	public Vector3 Axis{
		get{return axis; }
		set{axis = value; }
	}

	private Touch figers1;
	/// <summary>
	/// Gets or sets the figers.手指的一个数组
	/// </summary>
	/// <value>The figers.</value>
	public Touch Figers1
	{
		get{return figers1; }
		set{figers1 = value; }
	}

	private Touch figers2;
	/// <summary>
	/// Gets or sets the figers.手指的一个数组
	/// </summary>
	/// <value>The figers.</value>
	public Touch Figers2
	{
		get{return figers2; }
		set{figers2 = value; }
	}
		
	private Transform tarTransform=null;
	/// <summary>
	/// Gets or sets the target transform.射线打到的targets
	/// </summary>
	/// <value>The target transform.</value>
	public Transform TargetTransform{
		set{tarTransform = value; }
		get{return tarTransform; }
	}

	private Transform lastTransform=null;
	/// <summary>
	/// Gets or sets the last transform.上一次的transform
	/// </summary>
	/// <value>The last transform.</value>
	public Transform LastTransform
	{
		get{return lastTransform; }
		set{lastTransform = value; }
	}

	private MouseState myMouseState;
	/// <summary>
	/// Gets or sets the state of the my mouse.存取鼠标的状态
	/// </summary>
	/// <value>The state of the my mouse.</value>
	public MouseState MyMouseState
	{
		get{return myMouseState; }
		set{myMouseState = value; }
	}

	private TouchState myTouchState;
	/// <summary>
	/// Gets or sets the state of the my mouse.存取触屏的的状态
	/// </summary>
	/// <value>The state of the my mouse.</value>
	public TouchState MyTouchState
	{
		get{return myTouchState; }
		set{myTouchState = value; }
	}
}

/// <summary>
/// Mouse state.判断鼠标的状态
/// </summary>
public enum MouseState
{
	Idle,
	Drag,
	Scale,
	Rotate,
	staticState
}


/// <summary>
/// Touch state.存储touch的状态
/// </summary>
public enum TouchState
{
	Idle,
	Drag,
	Scale,
	Rotate,
	staticState
}

