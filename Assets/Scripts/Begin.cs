using System.Collections;
using System.Collections.Generic;
using Test;
using UnityEngine;

public class Begin : State
{
    public Begin(GameSystem gameSystem) : base(gameSystem)
    {
    }

    public override IEnumerator Start()
    {
        GameSystem.SetState(new PlayerTurn(GameSystem));
        yield break;
    }
}