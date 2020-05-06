using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Selection : MonoBehaviour
{
    
    [TagSelector]
    public string TagFilterAlly = "";
    
    [TagSelector]
    public string TagFilterEnemy = "";

    public LayerMask mask;
    
    public List<GameObject> allShips;
    
    public List<GameObject> allyShips;
    
    public GameObject ship;
    public GameObject choicePanel;
    
    
    public bool selected;

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


        if (Input.GetMouseButtonDown(0) && gameObject.GetComponent<Player>().selectable == true )
        {
            for (int i = 0; i < allShips.Count; i++)
            {
                Destroy(allShips[i].GetComponent<BoxCollider2D>());
                allShips[i].AddComponent<BoxCollider2D>();
            }
            
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, mask);
            if (hit.collider.tag == TagFilterAlly)
            {
                ship = hit.collider.gameObject;
                selected = true;
                choicePanel.SetActive(true);
            }
            else if (hit.collider.tag != "UI" && selected)
            {
                //ship = null;
                selected = false;
                choicePanel.SetActive(false);
            }
        }

        if (ship != null)
        {
            Vector3 rectTransform = new Vector3(ship.transform.position.x + 1, ship.transform.position.y + 1 , 0);
            choicePanel.transform.position = rectTransform;
        }
    }
}
