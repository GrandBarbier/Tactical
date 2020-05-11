using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationState : MonoBehaviour
{
    public bool neutre;
    public bool p1;
    public bool p2;
    public int health = 5;
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
            
        }
        else if (player.name == "Player 2")
        {
            gameObject.tag = "StationP2";
            health = 5;
        }
        
    }
}
