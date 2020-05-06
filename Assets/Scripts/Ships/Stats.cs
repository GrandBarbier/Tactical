using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public ScriptableObject shipStats;
    public bool moved = false;
    public bool attacked = false;
    public int health;
    public Text hpText;

    private void Update()
    {
        hpText.transform.position = transform.position;

        hpText.text =health.ToString();

        if (health <= 0)
        {
           gameObject.SetActive(false);
           hpText.gameObject.SetActive(false);
           gameObject.transform.position = new Vector3(1000,1000);
        }
    }
}