using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HaloHandler : MonoBehaviour
{
    public Sprite available;

    public Sprite moved;

    public Sprite attacked;

    private void Start()
    {

    }

    void Update()
    {
        //GetComponentInChildren<Image>().sprite = available;
        
        if (GetComponent<Stats>().moved)
        {
            GetComponentInChildren<Image>().sprite = moved;
            if (GetComponent<Stats>().attacked)
            {
                GetComponentInChildren<Image>().sprite = attacked;

            }
        }
        
    }

    private void OnMouseEnter()
    {
        GetComponentInChildren<Image>().enabled = true;
    }

    private void OnMouseExit()
    {
        GetComponentInChildren<Image>().enabled = false;
    }
}
