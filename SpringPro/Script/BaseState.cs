using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{

	public abstract void OnEnter(Transform tra);

	public abstract void OnStay(Transform tra);

	public abstract void OnExit(Transform tra);
}
