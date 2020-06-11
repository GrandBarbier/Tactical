using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Pathfinding;


public class Stats : MonoBehaviour
{
    public StatVaisseau ship;
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite dirigent1;
    public Sprite dirigent2;
    public bool moved = false;
    public bool attacked = false;
    public bool captured = false;
    public int health;
    public int shield;
    public int movePoints;
    public int damage;
    public int range;
    public int dmgMin, dmgMax;
    public AudioClip hitSound;
    public AudioClip explosionSound;
    public AudioClip shootSound;
    
    public AudioSource audioOnShips;

    public TextMeshProUGUI hpText;

    public GameObject player1;
    public GameObject player2;

    public Canvas canvas;

    public Animator animator;

    public AnimationClip deathClip;

    private Quaternion iniRot;
    private void Start()
    {
        sprite1 = ship.sprite1;
        sprite2 = ship.sprite2;

        dirigent1 = ship.dirigent1;
        dirigent2 = ship.dirigent2;

        hitSound = ship.hitSound;
        explosionSound = ship.explosionSound;
        shootSound = ship.shootSound;
        audioOnShips = ship.audioOnShips;

        iniRot = hpText.transform.rotation;
        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");
        health = ship.health;
        shield = ship.shield;
        movePoints = ship.mvt;
        dmgMin = ship.dmgMin;
        dmgMax = ship.dmgMax;
        range = ship.portée;

        audioOnShips = GetComponent<AudioSource>();
        
        if (gameObject.tag == "player1")
        {
            GetComponent<SpriteRenderer>().sprite = sprite1;
            animator.SetBool("isHuman", true);
        }
        if (gameObject.tag == "player2")
        {
            GetComponent<SpriteRenderer>().sprite = sprite2;
            animator.SetBool("isHuman", false);
        }
        
        animator.SetInteger("id", ship.id);




    }

    private void Update()
    {
        hpText.text =health.ToString();
        
        var moving = gameObject.GetComponent<AIPath>().velocity;

        if (moving.x > 0.1 || moving.y > 0.1 || moving.x < -0.1 || moving.y < -0.1)
        {
            animator.SetBool("moving",true);
        }
        else
        {
            animator.SetBool("moving",false);
        }

        if (health <= 0)
        {
            audioOnShips.clip = explosionSound;
            audioOnShips.Play();
            StartCoroutine(Death(deathClip.length));
        }
        hpText.transform.rotation = iniRot;

        damage = UnityEngine.Random.Range(ship.dmgMin, ship.dmgMax);
    }



    IEnumerator Death(float time)
    {
        animator.SetBool("dead",true);
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}