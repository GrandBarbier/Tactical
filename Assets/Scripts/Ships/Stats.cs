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

    private void Start()
    {
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
        hpText.transform.position = transform.position;

        hpText.text =health.ToString();

        if (health <= 0)
        {
            Destroy(hpText.gameObject);
            Destroy(gameObject);
        }
    }
}