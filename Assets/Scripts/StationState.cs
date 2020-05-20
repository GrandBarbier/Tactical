using System.Collections;
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
    public Text text;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        text.text = health.ToString();

        if (gameObject.tag == "StationP1" && health <= 0)
        {
            gameObject.tag = "Station";
            gameObject.GetComponent<SpriteRenderer>().sprite = station;
            health = newHealth;
        }
        else if(gameObject.tag == "StationP2" && health <= 0)
        {
            gameObject.tag = "Station";
            gameObject.GetComponent<SpriteRenderer>().sprite = station;
            health = newHealth;
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
        }
        
        else if (player.name == "Player 2")
        {
            gameObject.tag = "StationP2";
            health += newHealth;
            gameObject.GetComponent<SpriteRenderer>().sprite = station2;
        }
    }
}
