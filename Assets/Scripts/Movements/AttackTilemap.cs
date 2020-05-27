using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pathfinding;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class AttackTilemap : MonoBehaviour
{
    public Tilemap walkableTilemap;
    public Tilemap obstacleTilemap;
    
    public Vector3Int startTile;
    
    public TileBase selectedTile;
    public TileBase unselectedTile;
    
    public LayerMask mask;
    public LayerMask maskS;
    
    [TagSelector]
    public string TagFilterEnemy = "";

    [TagSelector]
    public string TagStationAlly = "";

    [TagSelector]
    public string TagStationEnnemy = "";

    [TagSelector]
    public string TagCoreStationEnnemy = "";

    [TagSelector]
    public string TagCoreStationAlly = "";

    public List<Vector3Int> targetable;
    
    public List<GameObject> allShips;
    public List<Vector3Int> allShipsPos;
    
    public List<GameObject> enemyShips;
    public List<Vector3Int> enemyShipPos;

    public List<GameObject> allyShips;
    public List<Vector3Int> allyshipPos;
    
    public Selection selection;

    public bool attacking;

    public GameObject attackButton;
    public GameObject captureButton;
    public GameObject moveButton;

    private void Start()
    {
        allShips = GetComponent<Selection>().allShips;

        ActualiseShipPos();

        selection = gameObject.GetComponent<Selection>();
    }

    void Update()
    {
        if (selection.ship.GetComponent<Stats>().moved == true)
        {
            moveButton.GetComponent<Image>().color = Color.grey;
        }
        else if (selection.ship.GetComponent<Stats>().moved == true)
        {
            moveButton.GetComponent<Image>().color = Color.white;
        }

        if (selection.ship.GetComponent<Stats>().attacked == true || selection.ship.GetComponent<Stats>().captured == true)
        {
            attackButton.GetComponent<Image>().color = Color.grey;
            captureButton.GetComponent<Image>().color = Color.grey;
            moveButton.GetComponent<Image>().color = Color.grey;
        }
        else
        {
            attackButton.GetComponent<Image>().color = Color.white;
            captureButton.GetComponent<Image>().color = Color.white;
            
        }

        enemyShips.Clear();
        enemyShips = enemyShips.Union(GameObject.FindGameObjectsWithTag(TagFilterEnemy)).ToList();
        enemyShips = enemyShips.Union(GameObject.FindGameObjectsWithTag("Station")).ToList();
        enemyShips = enemyShips.Union(GameObject.FindGameObjectsWithTag(TagStationEnnemy)).ToList();
        enemyShips = enemyShips.Union(GameObject.FindGameObjectsWithTag(TagCoreStationEnnemy)).ToList();
        allShips = selection.allShips;
        allyShips = selection.allyShips;
        allyShips = allyShips.Union(GameObject.FindGameObjectsWithTag(TagStationAlly)).ToList();
        allyShips = allyShips.Union(GameObject.FindGameObjectsWithTag(TagCoreStationAlly)).ToList();

        ActualiseShipPos();

        if (attacking && Input.GetMouseButton(0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int clickPos = walkableTilemap.WorldToCell(mouseWorldPos);
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, mask);
            RaycastHit2D hit2 = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, maskS);
            if (hit.collider != null)
            {
                if (hit.collider.tag == TagFilterEnemy && selection.ship.GetComponent<Stats>().attacked == false)
                {
                    
                    for (int i = 0; i < targetable.Count; i++)
                    {
                        if (clickPos == targetable[i])
                        {
                            for (int j = 0; j < enemyShipPos.Count; j++)
                            {
                                if (clickPos == enemyShipPos[j])
                                {
                                    hit.collider.gameObject.GetComponent<Stats>().health -= selection.ship.GetComponent<Stats>().damage;
                                    attacking = false;
                                    selection.ship.GetComponent<Stats>().attacked = true;
                                    selection.ship.GetComponent<Stats>().moved = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            if (hit2.collider != null)
            {
                Debug.Log(hit2.collider);
                if ((hit2.collider.tag == TagStationEnnemy || hit2.collider.tag == TagCoreStationEnnemy || hit2.collider.tag == "Station") && selection.ship.GetComponent<Stats>().attacked == false)
                {

                    hit2.collider.gameObject.GetComponent<StationState>().TakeDamage(selection.ship.GetComponent<Stats>().damage);
                    selection.ship.GetComponent<Stats>().attacked = true;
                    selection.ship.GetComponent<Stats>().moved = true;
                }
            }

            selection.deselected = true;
            ResetTilemap();
            attacking = false;
        }
    }
    
    
    public List<Vector3Int> GetWalkableTiles(int range, Vector3Int start)
    {
        if (range >= 1)
        {
            List<Vector3Int> walkable = new List<Vector3Int>();
            walkable.Add(start);
            for (int i = 0; i < range; i++)
            {
                foreach (var tile in walkable)
                {
                    walkable= walkable.Union(GetAdjacentTiles(tile)).ToList();
                }
            }
            walkable.Remove(start);
            return walkable;
        }
        else
        {
            return null;
        }
    }
    
    public List<Vector3Int> GetAdjacentTiles(Vector3Int tile)
    {
        List<Vector3Int> testTiles = new List<Vector3Int>();
        testTiles.Add(tile+Vector3Int.up);
        testTiles.Add(tile+Vector3Int.down);
        testTiles.Add(tile+Vector3Int.left);
        testTiles.Add(tile+Vector3Int.right);
        List<Vector3Int> tiles = new List<Vector3Int>();

        foreach (var vec in testTiles)
        {
             if (TargetableCheck(vec))
             {
                tiles.Add(vec);
             }
        }
        return tiles;
    }


    public bool TargetableCheck(Vector3Int tile)
    {
        for (int i = 0; i < allyshipPos.Count; i++)
        {
            if (tile == allyshipPos[i])
            {
                return false;
            }
        }
        if (obstacleTilemap.GetTile(tile) != null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    
    public void ResetTilemap()
    {
        foreach (var tile in targetable)
        {
            walkableTilemap.SetTile(tile,unselectedTile);
        }
        targetable.Clear();
    }
    
    void ColorWalkable()
    {
        foreach (var tile in targetable)
        {
            walkableTilemap.SetTile(tile,selectedTile);
        }
    }

    void ActualiseShipPos()
    {
        allShipsPos.Clear();
        enemyShipPos.Clear();
        allyshipPos.Clear();
        
        for (int i = 0; i < allShips.Count; i++)
        {
            Vector3Int pos = Vector3Int.FloorToInt(allShips[i].transform.position);
            allShipsPos.Add(pos);
        }
        
        for (int i = 0; i < enemyShips.Count; i++)
        {
            Vector3Int pos = Vector3Int.FloorToInt(enemyShips[i].transform.position);
            enemyShipPos.Add(pos);
        }
        
        for (int i = 0; i < allyShips.Count; i++)
        {
            Vector3Int pos = Vector3Int.FloorToInt(allyShips[i].transform.position);
            allyshipPos.Add(pos);
        }
    }


    public void Attacking()
    {
        if ( selection.selected == true && attacking == false)
        {
            ResetTilemap();
            if (selection.ship.GetComponent<Stats>().attacked == false)
            {
                startTile = walkableTilemap.WorldToCell(selection.ship.transform.position);
                targetable = GetWalkableTiles(selection.ship.GetComponent<Stats>().range, startTile);
                ColorWalkable();
                attacking = true;
                selection.choicePanel.SetActive(false);
               
            }
            
        }
    }
}