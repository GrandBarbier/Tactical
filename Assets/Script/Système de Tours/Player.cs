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
    }

    private void Update()
    {
        coreStationJ1 = GameObject.FindWithTag("CoreStationP1");
        coreStationJ2 = GameObject.FindWithTag("CoreStationP2");

        if (coreStationJ1.GetComponent<StationState>().health <= 0)
        {
            VictoryJ2.SetActive(true);
            Destroy(coreStationJ1);
            restart.SetActive(true);
        }

        if(coreStationJ2.GetComponent<StationState>().health <= 0)
        {
            VictoryJ1.SetActive(true);
            Destroy(coreStationJ2);
            restart.SetActive(true);
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
