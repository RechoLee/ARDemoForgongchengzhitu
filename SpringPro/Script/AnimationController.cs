using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// Animation controller.加在父物体上，用于控制子物体上的动画播放
/// </summary>
public class AnimationController : MonoBehaviour
{
	/// <summary>
	/// The children ani.存储子物体上的动画
	/// </summary>
	public Dictionary<string,DOTweenAnimation> childrenAni =new Dictionary<string, DOTweenAnimation>();

	void Start()
	{
		DOTweenAnimation[] anis = GetComponentsInChildren<DOTweenAnimation> ();

		for (int i = 0; i < anis.Length; i++) {
			childrenAni.Add (anis[i].name,anis[i]);
		}
	}
		
}
