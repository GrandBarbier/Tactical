using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Stats : MonoBehaviour
{
    public StatVaisseau ship;
    public Sprite sprite1;
    public Sprite sprite2;
    public bool moved = false;
    public bool attacked = false;
    public bool captured = false;
    public int health;
    public int shield;
    public int movePoints;
    public int damage;
    public int range;
    public int dmgMin, dmgMax;

    public TextMeshProUGUI hpText;

    public GameObject player1;
    public GameObject player2;

    public Canvas canvas;

    private Quaternion iniRot;
    private void Start()
    {
        sprite1 = ship.sprite1;
        sprite2 = ship.sprite2;
        iniRot = hpText.transform.rotation;
        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");
        health = ship.health;
        shield = ship.shield;
        movePoints = ship.mvt;
        dmgMin = ship.dmgMin;
        dmgMax = ship.dmgMax;
        range = ship.portée;
        if (gameObject.tag == "player1")
        {
            GetComponent<SpriteRenderer>().sprite = sprite1;
        }
        if (gameObject.tag == "player2")
        {
            GetComponent<SpriteRenderer>().sprite = sprite2;
        }
        

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

        damage = UnityEngine.Random.Range(ship.dmgMin, ship.dmgMax);
    }
    
}