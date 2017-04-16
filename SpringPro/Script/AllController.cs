using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllController : MonoBehaviour
{
	public static AllController instance=null;

	//一个存储transform和对应的mgr的字典
	Dictionary<Transform,StateManager> tsDic=new Dictionary<Transform, StateManager>();

	void Awake()
	{
		instance = this;
	}

	void Start()
	{
		TouchEventController.instance.ModelMouseDele = OnMouse;
		TouchEventController.instance.ModelTouchDele = OnTouch;
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
				} else {
					StateManager sm = CreateBaseState (e.TargetTransform);
					tsDic.Add (e.TargetTransform, sm);
					sm.ChangeState (new IdleState ());
				}
				break;
			case MouseState.Drag:
				//..
				StateManager sMgr2;
				if (tsDic.TryGetValue (e.TargetTransform, out sMgr2)) {
					sMgr2.ChangeState (new  DragState());
				} else {
					StateManager sm = CreateBaseState (e.TargetTransform);
					tsDic.Add (e.TargetTransform, sm);
					sm.ChangeState (new DragState ());
				}
				break;
			case MouseState.Rotate:
				//..
				StateManager sMgr3;
				if (tsDic.TryGetValue (e.TargetTransform, out sMgr3)) {
					sMgr3.ChangeState (new  RotateState());
				} else {
					StateManager sm = CreateBaseState (e.TargetTransform);
					tsDic.Add (e.TargetTransform, sm);
					sm.ChangeState (new RotateState ());
				}
				break;
			case MouseState.Scale:
				//..
				StateManager sMgr4;
				if (tsDic.TryGetValue (e.TargetTransform, out sMgr4)) {
					sMgr4.ChangeState (new ScaleState());
				} else {
					StateManager sm = CreateBaseState (e.TargetTransform);
					tsDic.Add (e.TargetTransform, sm);
					sm.ChangeState (new ScaleState ());
				}
				break;
			case MouseState.staticState:
				//..
				StateManager sMgr5;
				if (tsDic.TryGetValue (e.TargetTransform, out sMgr5)) {
					sMgr5.ChangeState (new StaticState());
				} else {
					StateManager sm = CreateBaseState (e.TargetTransform);
					tsDic.Add (e.TargetTransform, sm);
					sm.ChangeState (new StaticState());
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
				} else {
					StateManager sm = CreateBaseState (e.TargetTransform);
					tsDic.Add (e.TargetTransform, sm);
					sm.ChangeState (new IdleState ());
				}
				break;
			case TouchState.Drag:
			//..
				StateManager sMgr2;
				if (tsDic.TryGetValue (e.TargetTransform, out sMgr2)) {
					sMgr2.ChangeState (new DragState ());
				} else {
					StateManager sm = CreateBaseState (e.TargetTransform);
					tsDic.Add (e.TargetTransform, sm);
					sm.ChangeState (new DragState ());
				}
				break;
			case TouchState.Rotate:
			//..
				StateManager sMgr3;
				if (tsDic.TryGetValue (e.TargetTransform, out sMgr3)) {
					sMgr3.ChangeState (new RotateState ());
				} else {
					StateManager sm = CreateBaseState (e.TargetTransform);
					tsDic.Add (e.TargetTransform, sm);
					sm.ChangeState (new RotateState ());
				}
				break;
			case TouchState.Scale:
			//..
				StateManager sMgr4;
				if (tsDic.TryGetValue (e.TargetTransform, out sMgr4)) {
					sMgr4.ChangeState (new ScaleState ());
				} else {
					StateManager sm = CreateBaseState (e.TargetTransform);
					tsDic.Add (e.TargetTransform, sm);
					sm.ChangeState (new ScaleState ());
				}
				break;
			case TouchState.staticState:
				//..
				StateManager sMgr5;
				if (tsDic.TryGetValue (e.TargetTransform, out sMgr5)) {
					sMgr5.ChangeState (new StaticState());
				} else {
					StateManager sm = CreateBaseState (e.TargetTransform);
					tsDic.Add (e.TargetTransform, sm);
					sm.ChangeState (new StaticState());
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
	StateManager CreateBaseState(Transform tra)
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

}
