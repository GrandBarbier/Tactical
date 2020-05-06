using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pathfinding;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class AttackTilemap : MonoBehaviour
{
    public Tilemap walkableTilemap;
    public Tilemap obstacleTilemap;
    
    public Vector3Int startTile;
    
    public TileBase selectedTile;
    public TileBase unselectedTile;
    
    public LayerMask mask;
    
    [TagSelector]
    public string TagFilterAlly = "";
    
    [TagSelector]
    public string TagFilterEnemy = "";
    
    public int rangePoints;
    
    public List<Vector3Int> targetable;
    
    public List<GameObject> allShips;
    public List<Vector3Int> allShipsPos;
    
    public List<GameObject> enemyShips;
    public List<Vector3Int> enemyShipPos;
    
    public Selection selection;

    public bool attacking;

    public int damage;

    private void Start()
    {
        allShips = allShips.Union(GameObject.FindGameObjectsWithTag(TagFilterAlly)).ToList();

        ActualiseShipPos();

        selection = gameObject.GetComponent<Selection>();
    }

    void Update()
    {
        allShips = allShips.Union(GameObject.FindGameObjectsWithTag(TagFilterAlly)).ToList();

        enemyShips = enemyShips.Union(GameObject.FindGameObjectsWithTag(TagFilterEnemy)).ToList();
        ActualiseShipPos();

        if (attacking && Input.GetMouseButton(0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int clickPos = walkableTilemap.WorldToCell(mouseWorldPos);
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, mask);
            if (hit.collider.tag == TagFilterEnemy && attacking == true && selection.ship.GetComponent<Stats>().attacked == false)
            {
                for (int i = 0; i < targetable.Count; i++)
                {
                    if (clickPos == targetable[i])
                    {
                        for (int j = 0; j < enemyShipPos.Count; j++)
                        {
                            if (clickPos == enemyShipPos[j])
                            {
                                hit.collider.gameObject.GetComponent<Stats>().health -= damage;
                                attacking = false;
                                selection.ship.GetComponent<Stats>().attacked = true;
                            }
                        }
                    }
                }

                selection.selected = false;
                ResetTilemap();
                selection.ship = null; 
            }
            else if(selection.selected)
            {
                selection.selected = false;
                ResetTilemap();
                selection.ship = null;
                attacking = false;
            }
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
        for (int i = 0; i < allShipsPos.Count; i++)
        {
            if (tile == allShipsPos[i])
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
    }


    public void Attacking()
    {
        if ( selection.selected == true && attacking == false)
        {
            ResetTilemap();
            if (selection.ship.GetComponent<Stats>().attacked == false)
            {
                startTile = walkableTilemap.WorldToCell(selection.ship.transform.position);
                targetable = GetWalkableTiles(rangePoints, startTile);
                ColorWalkable();
                attacking = true;
                selection.choicePanel.SetActive(false);
            }
        }
    }
}