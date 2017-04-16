using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateModel : MonoBehaviour {

    private GameObject _mainCamera;


    void Start()
    {
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {
        //if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        //{
        //    Vector2 touchPos = Input.GetTouch(0).deltaPosition;
        //    if (Mathf.Abs(touchPos.y) > Mathf.Abs(touchPos.x))
        //    {
        //        transform.Rotate(new Vector3(0, -touchPos.x*500, 0),Space.World);
        //    }
        //}
        //单点触摸， 水平上下旋转  
        if (1 == Input.touchCount)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 deltaPos = touch.deltaPosition;
            transform.Rotate(Vector3.down * deltaPos.x, Space.World);
            transform.Rotate(Vector3.right * deltaPos.y, Space.World);
        }
    }
}
