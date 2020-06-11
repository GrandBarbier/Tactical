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
        if (GetComponentInChildren<Image>())
            GetComponentInChildren<Image>().enabled = false;
    }

    void Update()
    {
        if (GetComponent<Stats>().moved == false && GetComponent<Stats>().attacked == false)
            if (GetComponentInChildren<Image>())
                GetComponentInChildren<Image>().sprite = available;
        
        if (GetComponent<Stats>().moved)
        {
            if (GetComponentInChildren<Image>())
                GetComponentInChildren<Image>().sprite = moved;
            if (GetComponent<Stats>().attacked)
            {
                if (GetComponentInChildren<Image>())
                    GetComponentInChildren<Image>().sprite = attacked;

            }
        }
        
    }

    private void OnMouseEnter()
    {
        if (GetComponentInChildren<Image>())
            GetComponentInChildren<Image>().enabled = true;
    }

    private void OnMouseExit()
    {
        if (GetComponentInChildren<Image>())
            GetComponentInChildren<Image>().enabled = false;
    }
}
