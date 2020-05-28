﻿using System.Collections;
using System.Collections.Generic;
using Script.Système_de_Tours;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    
    public State currentState;

    public MovementsTilemap movementsTilemap;
    public AttackTilemap attackTilemp;
    public Station station;

    public bool selectable;
    public bool myTurn;

    public GameObject coreStationJ1;
    public GameObject coreStationJ2;

    public GameObject VictoryJ1;
    public GameObject VictoryJ2;

    public GameObject restart;

    


    private void Start()
    {
        SetState(new NotMyTurn(this));
        movementsTilemap = GetComponent<MovementsTilemap>();
        attackTilemp = GetComponent<AttackTilemap>();
        station = GetComponent<Station>();

        coreStationJ2 = GameObject.FindWithTag("CoreStationP2");
        coreStationJ1 = GameObject.FindWithTag("CoreStationP1");

        
    }

    public void Update()
    {        
        if (coreStationJ1.GetComponent<StationState>().health <= 0)
        {
            VictoryJ2.SetActive(true);
            coreStationJ1.GetComponent<Animator>().Play("CoreExplosion");
            Destroy(coreStationJ1, 1.0f);
            restart.SetActive(true);
            
        }

        
        if(coreStationJ2.GetComponent<StationState>().health <= 0)
        {
            VictoryJ1.SetActive(true);
            restart.SetActive(true);
            coreStationJ1.GetComponent<Animator>().Play("CoreExplosion");
            Destroy(coreStationJ2, 1.0f);
        }
    }

    public void SetState(State state)
    {
        if (currentState != null)
            currentState.OnStateExit();

        currentState = state;
        
        if (currentState != null)
            currentState.OnStateEnter();
    }

    public void ItsMyTurn()
    {
        SetState(new MyTurn(this));
    }

    public void ItsNotMyTurn()
    {
        SetState(new NotMyTurn(this));
    }
}
