using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState :BaseState
{
	#region implemented abstract members of BaseState

	public override void OnEnter (Transform tra)
	{
		if (tra != null) {
			tra.GetComponent<ModelBehaviour> ().isIdle = true;
		}
	}

	public override void OnStay (Transform tra)
	{

	}

	public override void OnExit (Transform tra)
	{
		if (tra != null) {
			tra.GetComponent<ModelBehaviour> ().isIdle = false;
		}
	}

	#endregion



}
