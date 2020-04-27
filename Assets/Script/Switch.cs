using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public List<GameObject> player1 = new List<GameObject>();
    public List<GameObject> player2 = new List<GameObject>();
    public bool FirstP;
    // Start is called before the first frame update
    void Start()
    {
        FirstP = true;

        foreach (GameObject player in GameObject.FindGameObjectsWithTag("player1"))
        {
           player1.Add(player);

        }

        foreach (GameObject player in GameObject.FindGameObjectsWithTag("player2"))
        {
           player2.Add(player);

        }

        for (int i = 0; i < player1.Count; i++)
        {
            player1[i].GetComponent<Move>().enabled = true;
            player2[i].GetComponent<Move>().enabled = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SwitchPlayer()
    {
        if (FirstP == true)
        {
            for (int i = 0; i < player1.Count; i++)
            {
                player1[i].GetComponent<Move>().enabled = false;
                player2[i].GetComponent<Move>().enabled = true;
            }
            
            FirstP = false;
        }
        else
        {
            for (int i = 0; i < player2.Count; i++)
            {
                player2[i].GetComponent<Move>().enabled = false;
                player1[i].GetComponent<Move>().enabled = true;
            }
            FirstP = true;
        }

        

    }
}
