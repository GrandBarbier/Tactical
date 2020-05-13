using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationState : MonoBehaviour
{
   
    public int health = 5;

    public Sprite Station1;
    public Sprite Station2;
    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        
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
            health = 5;
            gameObject.GetComponent<SpriteRenderer>().sprite = Station1;
            
        }
        else if (player.name == "Player 2")
        {
            gameObject.tag = "StationP2";
            health = 5;
            gameObject.GetComponent<SpriteRenderer>().sprite = Station1;
        }
        
    }
}
