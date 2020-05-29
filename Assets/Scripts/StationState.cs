using System.Collections;
using System.Collections.Generic;
using Script.Système_de_Tours;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StationState : MonoBehaviour
{
   
    public int health;
    public int baseHealth;
    public int newHealth;

    public Sprite station;
    public Sprite station1;
    public Sprite station2;
    public Sprite stationCore2;
    public TextMeshProUGUI text;
    public Animator animator;

    public bool spawned;
    public Station stationScript;
    public TurnManager turnManager;
    
    
    
    void Start()
    {
        turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
        stationScript = turnManager.players[turnManager.actualTurn].GetComponent<Station>();
        
      if (gameObject.tag == "CoreStationP1")
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = station1;
            health = health + 10;
        }
        if (gameObject.tag == "CoreStationP2")
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = stationCore2;
            health = health + 10;
        }
        if (gameObject.tag == "StationP1")
        {
            animator.Play("HumanNeutre");
            health = health + 5;
        }
        if (gameObject.tag == "StationP2")
        {
            animator.Play("AlienNeutre");
            health = health + 5;
        }
    }

    
    void Update()
    {
        if (gameObject.tag == "CoreStationP2" && health <= 0)
        {
            animator.SetBool("Destroy", true);
            
        }
        if (gameObject.tag == "CoreStationP1" && health <= 0)
        {
            animator.SetBool("Destroy", true);

        }
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
            animator.Play("HumanNeutre");
            gameObject.tag = "StationP1";
            health += newHealth;
            gameObject.GetComponent<SpriteRenderer>().sprite = station1;
        }
        
        else if (player.name == "Player 2")
        {
            animator.Play("AlienNeutre");
            gameObject.tag = "StationP2";
            health += newHealth;
            gameObject.GetComponent<SpriteRenderer>().sprite = station2;
           
        }
    }
}
