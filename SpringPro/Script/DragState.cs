using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragState :BaseState
{
	#region implemented abstract members of BaseState

	public override void OnEnter (Transform tra)
	{
		if (tra != null) {
			tra.GetComponent<ModelBehaviour> ().isDrag = true;
		}
	}

	public override void OnStay (Transform tra)
	{
		
	}

	public override void OnExit (Transform tra)
	{
		if (tra != null) {
			tra.GetComponent<ModelBehaviour> ().isDrag = false;
		}
	}

	#endregion


}
