using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Test
{
	public class PlayerTurn : State
	{
		public PlayerTurn(GameSystem gameSystem) : base(gameSystem)
		{
		}

		public override IEnumerator Start()
		{
			yield break;
		}
	}
}