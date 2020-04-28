using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Script.Système_de_Tours
{
    public class TurnManager : MonoBehaviour
    {
        public static TurnManager Instance;
    
        public List<Player> actualPlayers = new List<Player>();

        public int actualTurn;

        public TextMeshProUGUI turnText;
        public TextMeshProUGUI p1State;
        public TextMeshProUGUI p2State;

        void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            } else if (Instance != null)
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            actualTurn = 0;
            actualPlayers[actualTurn].ItsMyTurn();
        }

        private void Update()
        {
            turnText.text = "C'est le tour de " + actualPlayers[actualTurn].name;
            p1State.text = "P1 : " + actualPlayers[0].currentState;
            p2State.text = "P2 : " + actualPlayers[1].currentState;
        }

        public void NextTurn()
        {
            actualPlayers[actualTurn].ItsNotMyTurn();
            actualTurn ++;
            if (actualTurn > actualPlayers.Count - 1)
            {
                actualTurn = 0;
            }
            actualPlayers[actualTurn].ItsMyTurn();
        }
    }
}
