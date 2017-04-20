using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject exitMessagePrefab;
    public GameObject exitMessage;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (exitMessage == null)
            {
                exitMessage = Instantiate(exitMessagePrefab) as GameObject;
                StartCoroutine("resetQuitMessage");
            }

        }

    }
    IEnumerator resetQuitMessage()
    {
        yield return new WaitForSeconds(1.0f);
        if (exitMessage != null)
        {
            Destroy(exitMessage);
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}
