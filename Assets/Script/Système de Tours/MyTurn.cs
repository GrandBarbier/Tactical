namespace Script.Système_de_Tours
{
    public class MyTurn : State
    {
        public MyTurn(Player player) : base(player)
        {
            
        }
        public override void Tick()
        {
        
        }
        public override void OnStateEnter()
        {
            for (int i = 0; i < player.gameObject.GetComponent<MovementsTilemap>().allyShips.Count; i++)
            {
                player.gameObject.GetComponent<MovementsTilemap>().allShips[i].GetComponent<Stats>().moved = false;
            }
        }
    
        public override void OnStateExit()
        {
            player.movementsTilemap.enabled = false;
        }
    }
}
