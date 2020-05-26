using UnityEngine.UI;
using UnityEngine;
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

            player.station.money = player.station.money + 1500 + (500 * player.station.allyStation.Count);
        }
    
        public override void OnStateExit()
        {
            player.movementsTilemap.enabled = false;
            player.attackTilemp.enabled = false;
            player.selectable = false;
            player.station.turn = false;
            player.station.enabled = false;

            player.gameObject.GetComponent<AttackTilemap>().attackButton.GetComponent<Image>().color = Color.white;
            player.gameObject.GetComponent<AttackTilemap>().captureButton.GetComponent<Image>().color = Color.white;
            player.gameObject.GetComponent<AttackTilemap>().moveButton.GetComponent<Image>().color = Color.white;




        }
    }
}
