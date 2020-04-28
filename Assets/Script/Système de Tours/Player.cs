using System.Collections;
using System.Collections.Generic;
using Script.Système_de_Tours;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    
    public State currentState;
    public GameObject reglette;

    private bool _firsSet;
    
    private void Start()
    {
        SetState(new NotMyTurn(this));
        _firsSet = false;
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
        if(_firsSet)
            SetState(new MyTurn(this));
        else
        {
            SetState(new FirstSet(this));
        }
    }

    public void ItsNotMyTurn()
    {
        SetState(new NotMyTurn(this));
    }
}
