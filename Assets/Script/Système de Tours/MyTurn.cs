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
            
            player.myTurn = true;
                
            for (int i = 0; i < player.gameObject.GetComponent<Selection>().allyShips.Count; i++)
            {
                player.gameObject.GetComponent<Selection>().allyShips[i].GetComponent<Stats>().moved = false;
                player.gameObject.GetComponent<Selection>().allyShips[i].GetComponent<Stats>().attacked = false;
            }

            player.selectable = true;

            player.station.turn = true;
            player.station.spawned = false;
            player.station.enabled = true;
        }
    
        public override void OnStateExit()
        {
            player.movementsTilemap.enabled = false;
            player.attackTilemp.enabled = false;
            player.selectable = false;
            player.station.turn = false;
            player.station.enabled = false;

        }
    }
}
