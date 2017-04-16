using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CloseTextPanel : MonoBehaviour
{



	public Tweener panelMove;

	public void ClosePanelController()
	{
		panelMove.PlayBackwards ();
	}
}