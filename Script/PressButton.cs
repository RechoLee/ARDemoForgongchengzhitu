using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressButton : MonoBehaviour {

    public Image[] icon=new Image[4];
    public Sprite[] sprite = new Sprite[4];
    public GameObject[] Panel = new GameObject[4];
    public GameObject[] Pan_Model = new GameObject[2];
    public GameObject Model;

    public GameObject PanelOne;

    private int i = 0;

    void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }

    void Start()
    {
        
    }

    public void PressButtonHome()
    {
        icon[0].sprite = sprite[4];
        icon[1].sprite = sprite[1];
        icon[2].sprite = sprite[2];
        icon[3].sprite = sprite[3];
        Panel[0].SetActive(true);
        Panel[1].SetActive(false);
        Panel[2].SetActive(false);
        Panel[4].SetActive(false);
    }

    public void PressText()
    {
            PanelOne.SetActive(true);
    }
    public void PressButtonBook()
    {
        icon[0].sprite = sprite[0];
        icon[1].sprite = sprite[5];
        icon[2].sprite = sprite[2];
        icon[3].sprite = sprite[3];
        Panel[0].SetActive(false);
        Panel[1].SetActive(true);
        Panel[2].SetActive(false);
        Panel[4].SetActive(false);
    }

    public void PressPicture()
    {
        Pan_Model[0].SetActive(true);
    }

    public void PressAnswer()
    {
        int i = 0;
        if (i % 2 == 0)
        {
            Pan_Model[1].SetActive(true);
            Model.SetActive(false);
            i++;
        }
        else
        {
            Pan_Model[1].SetActive(false);
            i++;
        }
    }

    public void PressButtonMore()
    {
        icon[0].sprite = sprite[0];
        icon[1].sprite = sprite[1];
        icon[2].sprite = sprite[2];
        icon[3].sprite = sprite[6];
        Panel[0].SetActive(false);
        Panel[1].SetActive(false);
        if(i%2==0)
            Panel[2].SetActive(true);
        else
        Panel[4].SetActive(true);
    }

    public void PressButtonSign()
    {
        Panel[3].SetActive(true);
    }

    public void PressSignIn()
    {
        Panel[3].SetActive(false);
        Panel[4].SetActive(true);
        i=1;
    }

    public void PressBack()
    {
        Panel[3].SetActive(false);
    }
}
