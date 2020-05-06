namespace Script.Système_de_Tours
{
    public class NotMyTurn : State
    {
        public NotMyTurn(Player player) : base(player)
        {
        }
        public override void Tick()
        {
        
        }
        public override void OnStateEnter()
        {
            player.myTurn = false;
        }
    
        public override void OnStateExit()
        {
            player.movementsTilemap.enabled = true;
            player.attackTilemp.enabled = true;
        }
    }
}
