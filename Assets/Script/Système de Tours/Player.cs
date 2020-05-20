using System.Collections;
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

    public GameObject coreShipJ1;
    public GameObject coreShipJ2;

    public GameObject VictoryJ1;
    public GameObject VictoryJ2;



    private void Start()
    {
        SetState(new NotMyTurn(this));
        movementsTilemap = GetComponent<MovementsTilemap>();
        attackTilemp = GetComponent<AttackTilemap>();
        station = GetComponent<Station>();
    }

    private void Update()
    {
        coreShipJ1 = GameObject.Find("CoreShip1");
        coreShipJ2 = GameObject.Find("CoreShip1");

        if (coreShipJ1.GetComponent<Stats>().health <= 0)
        {
            VictoryJ2.SetActive(true);
        }

        if(coreShipJ2.GetComponent<Stats>().health <= 0)
        {
            VictoryJ1.SetActive(true);
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
