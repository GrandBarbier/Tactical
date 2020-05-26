﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StationState : MonoBehaviour
{
   
    public int health;
    public int baseHealth;
    public int newHealth;

    public Sprite station;
    public Sprite station1;
    public Sprite station2;
    public Sprite stationCore2;
    public Text text;
    public Animator animator;
    
    void Start()
    {
      if (gameObject.tag == "CoreStationP1")
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = station1;
            health = health + 5;
        }
        if (gameObject.tag == "CoreStationP2")
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = stationCore2;
            health = health + 5;
        }
    }

    
    void Update()
    {
        text.text = health.ToString();

        if (gameObject.tag == "StationP1" && health <= 0)
        {
            gameObject.tag = "Station";
            gameObject.GetComponent<SpriteRenderer>().sprite = station;
            health = newHealth;
            animator.Play("IdleNeutre");
        }
        else if(gameObject.tag == "StationP2" && health <= 0)
        {
            gameObject.tag = "Station";
            gameObject.GetComponent<SpriteRenderer>().sprite = station;
            health = newHealth;
            animator.Play("IdleNeutre");
        }
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg; 
    }
    
    public void Capture(GameObject player)
    {
        if(player.name == "Player 1")
        {
            gameObject.tag = "StationP1";
            health += newHealth;
            gameObject.GetComponent<SpriteRenderer>().sprite = station1;
            animator.Play("HumanNeutre");
        }
        
        else if (player.name == "Player 2")
        {
            gameObject.tag = "StationP2";
            health += newHealth;
            gameObject.GetComponent<SpriteRenderer>().sprite = station2;
            animator.Play("AlienNeutre");
        }
    }
}
