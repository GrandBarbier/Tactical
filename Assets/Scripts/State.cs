using System.Collections;
using UnityEngine;

namespace Test
{
	public abstract class State : MonoBehaviour
	{
		protected GameSystem GameSystem;

		protected State(GameSystem gameSystem)
		{
			GameSystem = gameSystem;
		}

		public virtual IEnumerator Start()
		{
			yield break;
		}
	}
}

