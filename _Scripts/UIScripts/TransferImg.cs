using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TransferImg : MonoBehaviour
{
	public Image transferBG;

	void Start()
	{
		TransferToBg ();
	}

	void TransferToBg()
	{
		transferBG.DOFade (0f, 8f);
	}

}