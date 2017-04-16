using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TagToPanel : MonoBehaviour
{
	public RectTransform textPanel;
	public Tweener panelMove;

	private CloseTextPanel closeBtn;

	void Start()
	{
		closeBtn = this.GetComponent<CloseTextPanel> ();
	}

	public void OnClickTagMoveText()
	{

		//面板移动
		if (panelMove == null) {
			panelMove = textPanel.DOLocalMoveX (0f, 1.5f);
			panelMove.SetAutoKill (false);
			closeBtn.panelMove = this.panelMove;
		} 
		panelMove.PlayForward ();

	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Space)) {
			OnClickTagMoveText ();
		}
	}
}