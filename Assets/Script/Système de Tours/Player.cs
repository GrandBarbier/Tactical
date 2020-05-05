﻿using System.Collections;
using System.Collections.Generic;
using Script.Système_de_Tours;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    
    public State currentState;

    public MovementsTilemap movementsTilemap;

    public bool selectable;
    public bool myTurn;


    private void Start()
    {
        SetState(new NotMyTurn(this));
        movementsTilemap = GetComponent<MovementsTilemap>(); 
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
