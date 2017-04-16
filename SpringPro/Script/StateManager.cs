using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateManager
{
	//当前状态
	BaseState currentState = null;

	//操作的对象
	private Transform traTarget;

	//状态集合
	List<BaseState> listState = new List<BaseState> ();


	public StateManager()
	{

	}

	public StateManager(Transform transform)
	{
		traTarget = transform;
	}

	/// <summary>
	/// Add the specified state.增加一个状态
	/// </summary>
	/// <param name="state">State.</param>
	public void Add(BaseState state)
	{
		if (!listState.Contains (state)) {
			listState.Add (state);
			if (listState.Count == 1) {
				currentState = state;
			}
		}
	}

	/// <summary>
	/// Remove the specified state.移除一个状态
	/// </summary>
	/// <param name="state">State.</param>
	public void Remove(BaseState state)
	{
		if (listState.Contains (state)) {
			listState.Remove (state);
			if (listState.Count == 0) {
				currentState = null;
			}
		}
	}

	/// <summary>
	/// Changes the state.遍历集合是否存在state，存在则把状态改变
	/// </summary>
	/// <param name="state">State.</param>
	public void ChangeState(BaseState state)
	{
		if (listState.Contains (state)) {
			currentState.OnExit (traTarget);
			currentState = state;
			currentState.OnEnter (traTarget);
		} else {
			if (currentState != null){
				currentState.OnExit (traTarget);
			}
			listState.Add (state);
			currentState = state;
			currentState.OnEnter (traTarget);
		}
	}


}
