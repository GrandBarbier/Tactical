using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public StatVaisseau ship;
    public bool moved = false;
    public bool attacked = false;
    public int health;
    public int shield;
    public int movePoints;
    public int damage;
    public int range;
    
    public Text hpText;

    public GameObject player1;
    public GameObject player2;

    public Canvas canvas;

    private Quaternion iniRot;
    private void Start()
    {
        iniRot = hpText.transform.rotation;
        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");
        health = ship.health;
        shield = ship.shield;
        movePoints = ship.mvt;
        damage = ship.dmg;
        range = ship.portée;
    }

    private void Update()
    {
        hpText.text =health.ToString();

        if (health <= 0)
        {
            Destroy(hpText.gameObject);
            Destroy(gameObject);
        }
        hpText.transform.rotation = iniRot;
    }
    
}