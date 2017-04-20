using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateModel : MonoBehaviour {

    private Vector2 _TouchArea;

    //private Vector2 _FirstFinger;
    //private Vector2 _SecondFinger;
    private float _Distance;

    private Touch oldTouch1;
    private Touch oldTouch2;

    public float maxSize = 1.2f;
    public float minSize = 0.3f;

    void Start()
    {
        _TouchArea = Vector2.zero;
    }


    void Update()
    {

        //单点触摸， 水平上下旋转  
        /*if (1 == Input.touchCount)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 deltaPos = touch.deltaPosition;
            transform.Rotate(Vector3.down * deltaPos.x, Space.World);
            transform.Rotate(Vector3.right * deltaPos.y, Space.World);
        }*/

        if (Input.touchCount <= 0)
        {
            return;
        }

        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchPos = Input.GetTouch(0).deltaPosition;
            if (Mathf.Abs(touchPos.y) > Mathf.Abs(touchPos.x))
            {
                transform.Rotate(new Vector3(touchPos.y, 0, 0), Space.World);
            }
            else
            {
                transform.Rotate(new Vector3(0, -touchPos.x, 0), Space.World);
            }
        }



        if (Input.touchCount > 1 && ((Input.GetTouch(0).phase == TouchPhase.Moved) || (Input.GetTouch(1).phase == TouchPhase.Moved)))
        {

           
            Touch newTouch1 = Input.GetTouch(0);
            Touch newTouch2 = Input.GetTouch(1);

            //第2点刚开始接触屏幕, 只记录，不做处理  
            if (newTouch2.phase == TouchPhase.Began)
            {
                oldTouch2 = newTouch2;
                oldTouch1 = newTouch1;
                return;
            }

            float oldDistance = Vector2.Distance(oldTouch1.position, oldTouch2.position);
            float newDistance = Vector2.Distance(newTouch1.position, newTouch2.position);

            //两个距离之差，为正表示放大手势， 为负表示缩小手势  
            float offset = newDistance - oldDistance;

            //放大因子， 一个像素按 0.01倍来算(100可调整)  
            float scaleFactor = offset / 500f;
            Vector3 localScale = transform.localScale;
            Vector3 scale = new Vector3(localScale.x + scaleFactor,
                                        localScale.y + scaleFactor,
                                        localScale.z + scaleFactor);

            //最小缩放到 0.3 倍  
            if (scale.x > minSize && scale.y > minSize && scale.z > minSize && scale.x < maxSize && scale.y < maxSize && scale.z < maxSize)
            {
                transform.localScale = scale;
            }

            //记住最新的触摸点，下次使用  
            oldTouch1 = newTouch1;
            oldTouch2 = newTouch2;
        }
    }
}
