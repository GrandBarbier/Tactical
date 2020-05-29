using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Selection : MonoBehaviour
{
    
    [TagSelector]
    public string TagFilterAlly = "";
    
    [TagSelector]
    public string TagFilterEnemy = "";



    public LayerMask mask;
    public LayerMask uiMask;
    
    public List<GameObject> allShips;
    
    public List<GameObject> allyShips;
    
    public GameObject ship;
    public GameObject choicePanel;

    public Text Stat;
   
    
    public bool selected;

    public bool deselected;

    public bool ui;
    private void Start()
    {
        choicePanel.SetActive(false);
    }

    void Update()
    {
        allShips.Clear();
        allyShips.Clear();
        allyShips = allyShips.Union(GameObject.FindGameObjectsWithTag(TagFilterAlly)).ToList();
        allShips = allShips.Union(GameObject.FindGameObjectsWithTag(TagFilterAlly)).ToList();
        allShips = allShips.Union(GameObject.FindGameObjectsWithTag(TagFilterEnemy)).ToList();
        allShips = allShips.Union(GameObject.FindGameObjectsWithTag("StationP1")).ToList();

        if (deselected)
        {
            selected = false;
            deselected = false;
            choicePanel.SetActive(false);
            gameObject.GetComponent<MovementsTilemap>().moving = false;
            ship = null;
        }

        
        if (Input.GetMouseButtonDown(0) && gameObject.GetComponent<Player>().selectable == true )
        {
            
            for (int i = 0; i < allShips.Count; i++)
            {
                Destroy(allShips[i].GetComponent<BoxCollider2D>());
                allShips[i].AddComponent<BoxCollider2D>();
            }

            if (!selected)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, mask);
                if (hit.collider != null)
                {
                    if (hit.collider.tag == TagFilterAlly)
                    {
                        ship = hit.collider.gameObject;
                        selected = true;
                        choicePanel.SetActive(true);
                        Stat.GetComponent<Text>().text = "Damage : " + ship.GetComponent<Stats>().dmgMin.ToString() + "-" + ship.GetComponent<Stats>().dmgMax.ToString();
                    }
                }
                else
                {
                    deselected = true;
                    Stat.GetComponent<Text>().text = "";
                }
            }
            else
            {
                RaycastHit2D uiHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, uiMask);
                if (uiHit.collider == null)
                {
                    deselected = true;
                }
            }
        }
        
        if (ship != null)
        {
            Vector3 rectTransform = new Vector3(ship.transform.position.x + 1, ship.transform.position.y + 1 , 0);
            choicePanel.transform.position = rectTransform;
        }
    }
}
