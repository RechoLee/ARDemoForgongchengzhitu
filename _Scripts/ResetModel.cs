using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ResetModel : MonoBehaviour
{	
	/// <summary>
	/// Resets the position.重置位置信息
	/// </summary>
	/// <param name="pos">Position.</param>
	public void ResetPos(Vector3 pos)
	{
		transform.DOLocalMove (pos,0.5f);
	}

	/// <summary>
	/// Resets the rotation.重置旋转
	/// </summary>
	/// <param name="rot">Rot.</param>
	public void ResetRotation(Vector3 rot)
	{
		transform.DOLocalRotate (rot,0.5f);
	}

	public void ResetScale(Vector3 scale)
	{
		transform.DOScale (scale,0.5f);
	}
}
