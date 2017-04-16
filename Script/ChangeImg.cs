using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImg : MonoBehaviour {

    public Sprite[] Images;
    private int i = 0;
	
	void Start () {
        StartCoroutine(ChangeImages());
	}

    IEnumerator ChangeImages()
    {
        while (true)
        {
            this.GetComponent<Image>().sprite = Images[i];
            yield return new WaitForSeconds(2f);
            if (i == 2)
                i = 0;
            else
                i++;
        }
    }
	
}
