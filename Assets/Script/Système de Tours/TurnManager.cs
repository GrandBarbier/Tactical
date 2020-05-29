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

        public GameObject interface1;
        public GameObject interface2;

        public Sprite NextHuman;
        public Sprite NextAlien;

        public GameObject buttonNext;

        public Animator Alien;
        public Animator Human;


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
            turnText.text = players[actualTurn].name;

            if (players[actualTurn].name == "Player 1")
            {
                Human.SetBool("Tour", true);
                interface1.SetActive(true);
                interface2.SetActive(false);
                buttonNext.GetComponent<Image>().sprite = NextHuman;
            }
            else
            {
                Alien.SetBool("Tour", true);
                interface1.SetActive(false);
                interface2.SetActive(true);
                buttonNext.GetComponent<Image>().sprite = NextAlien;
            }
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
