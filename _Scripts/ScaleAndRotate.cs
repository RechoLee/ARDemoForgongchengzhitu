using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scale and rotate.用于对象的旋转和缩放
/// </summary>
public class ScaleAndRotate : MonoBehaviour
{
	public bool isCollider = false;

	/// <summary>
	/// The rotate speed.旋转的速度
	/// </summary>
	public float rotateSpeed=250f;

	public bool isRoteteSelf =false;

	public bool isDragMove =false;

	/// <summary>
	/// Rotates the and scale.旋转和缩放
	/// </summary>

	void Update()
	{
		Rotate ();
		if (isRoteteSelf&&!isDragMove&&!isCollider)
		{
			transform.Rotate (new Vector3(1,1,0)*Time.deltaTime*30,Space.Self);
		}
	}


	void Rotate()
	{
		#if UNITY_EDITOR||UNITY_EDITOR_WIN
		RotateOnWin();
		ScaleOnWin();
		#elif UNITY_ANDROID
		RotateOnMobile();
		ScaleOnMobile();
		#endif


	}


	void RotateOnWin()
	{
		if (!isDragMove&&!isRoteteSelf&&!isCollider) {
			float horizontal = Input.GetAxis ("Mouse X");
			float vertical = Input.GetAxis ("Mouse Y");
			Vector3 moveAxis = new Vector3 (horizontal, vertical, 0f);
			Vector3 rotateAxis = Vector3.Cross (moveAxis, Vector3.forward);

			this.transform.Rotate (rotateAxis, rotateSpeed * Time.deltaTime, Space.World);
		}


	}


	void ScaleOnWin()
	{
		if (!isDragMove&&!isRoteteSelf&&!isCollider) {
			float scaleValue = Input.GetAxis ("Mouse ScrollWheel");

			if (scaleValue > 0) {
				this.transform.localScale *= 1.1f;
			}

			if (scaleValue < 0) {
				this.transform.localScale *= 0.9f;
			}

		}
	}
		

	/// <summary>
	/// Rotates the on mobile.手机上的旋转
	/// </summary>
	void RotateOnMobile()
	{
		if (!isDragMove&&!isRoteteSelf&&!isCollider) {
			if (Input.touchCount > 0 && TouchPhase.Moved == Input.GetTouch (0).phase) {
				Vector2 figerVec = Input.GetTouch (0).deltaPosition;
				Vector3 moveAxis = new Vector3 (figerVec.x, figerVec.y, 0);

				Vector3 rotateAxis = Vector3.Cross (moveAxis, Camera.main.transform.forward);

				this.transform.Rotate (rotateAxis, rotateSpeed * Time.deltaTime,Space.World);
			}
		}
	}



	Vector2 foreVec=Vector2.zero;
	Vector2 laterVec=Vector2.zero;
	/// <summary>
	/// Scales the on mobile.手机上的缩放
	/// </summary>
	void ScaleOnMobile()
	{
		if (!isDragMove&&!isRoteteSelf&&!isCollider) {
			if (Input.touchCount == 2) {
				Touch figer1 = Input.GetTouch (0);
				Touch figer2 = Input.GetTouch (1);
				if (figer1.phase == TouchPhase.Stationary && figer2.phase == TouchPhase.Stationary) {
					foreVec = figer1.position - figer2.position;
				}
				if (figer1.phase == TouchPhase.Moved || figer2.phase == TouchPhase.Moved) {
					laterVec = figer1.position - figer2.position;
					if (foreVec.sqrMagnitude > laterVec.sqrMagnitude) {
						this.transform.localScale *= 0.95f;
					} else if (foreVec.sqrMagnitude < laterVec.sqrMagnitude) {
						this.transform.localScale *= 1.05f;
					} else {
						return;
					}
				}
			}
		}
	}
}
