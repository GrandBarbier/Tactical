using System;

namespace Test
{
	public class GameSystem : StateMachine
	{
		private void Start()
		{
			SetState(new Begin(this));
		}
	}
}
