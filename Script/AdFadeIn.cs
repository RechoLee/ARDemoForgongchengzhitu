using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdFadeIn : MonoBehaviour {

    void Start()
    {

        Component[] comps = this.GetComponentsInChildren<Component>();
        foreach (Component c in comps)
        {
            if (c is Graphic)
            {
                (c as Graphic).CrossFadeAlpha(0, 5, true);
                StartCoroutine(Wait());
            }

        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5f);
        this.gameObject.SetActive(false);
    }
    
}
