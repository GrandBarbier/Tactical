using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Système_de_Tours
{
    public class TurnManager : MonoBehaviour
    {
        public static TurnManager Instance;
    
        public List<GameObject> players ;

        public int actualTurn;

        public Text turnText;

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
            players[actualTurn].GetComponent<Player>().ItsMyTurn();
        }

        private void Update()
        {
            turnText.text = "Tour de " + players[actualTurn].name;
        }

        public void NextTurn()
        {
            players[actualTurn].GetComponent<MovementsTilemap>().ResetTilemap();
            players[actualTurn].GetComponent<AttackTilemap>().ResetTilemap();
            players[actualTurn].GetComponent<Selection>().selected = false;
            players[actualTurn].GetComponent<Player>().ItsNotMyTurn();
            actualTurn ++;
            if (actualTurn > players.Count - 1)
            {
                actualTurn = 0;
            }
            players[actualTurn].GetComponent<Player>().ItsMyTurn();
        }
    }
}
