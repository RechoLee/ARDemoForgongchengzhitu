using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderPanel : MonoBehaviour {

    private float FloSlingSmoothPara = 3F;
	
	// Update is called once per frame
	void Update () {
        if (this.transform.localPosition.x < 0 && this.transform.localPosition.x > -800)
        {
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, new Vector3(-800, 0, 0), Time.deltaTime * FloSlingSmoothPara);
            if (Mathf.RoundToInt(this.transform.localPosition.x) == -800)
            {
                this.transform.localPosition = new Vector3(-800, 0, 0);
            }
        }
        else if (this.transform.localPosition.x < -800 && this.transform.localPosition.x > -1600)
        {
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, new Vector3(-1600, 0, 0), Time.deltaTime * FloSlingSmoothPara);
            if (Mathf.RoundToInt(this.transform.localPosition.x) == -1600)
            {
                this.transform.localPosition = new Vector3(-1600, 0, 0);
            }
        }
    }
}
