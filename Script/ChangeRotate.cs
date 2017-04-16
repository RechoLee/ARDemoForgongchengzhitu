using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRotate : MonoBehaviour {

    
    public GameObject Model;

    public void RotateAngle()
    {
        int i = 0;
        if (i % 3 == 0)
        { 
            Model.transform.Rotate(-90, 0, 0);
            Debug.Log("0");
            i++;
            return;
        }
        if (i % 3 == 1)
        {
            transform.Rotate(-90, 0, 0);
            Debug.Log("1");
            i++;
            return;
        }
        if (i % 3 == 2)
        {
            transform.Rotate(-90,90, 0);
            Debug.Log("2");
            i++;
            return;
        }

    }
}
