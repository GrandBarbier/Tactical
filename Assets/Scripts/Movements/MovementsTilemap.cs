using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pathfinding;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class MovementsTilemap : MonoBehaviour
{
    public Tilemap walkableTilemap;
    public Tilemap obstacleTilemap;
    
    public Vector3Int startTile;
    
    public TileBase selectedTile;
    public TileBase unselectedTile;
    
    public LayerMask mask;

    public GameObject destination;
    
    
    [TagSelector]
    public string TagFilterAlly = "";
    
    [TagSelector]
    public string TagFilterEnemy = "";
    
    public int movePoints;
    
    public List<Vector3Int> walkable;
    
    public List<GameObject> allShips;
    public List<Vector3Int> allShipsPos;
    
    public Selection selection;

    public bool moving;

    private void Start()
    {
        allShips = GetComponent<Selection>().allShips;
        ActualiseShipPos();

        selection = gameObject.GetComponent<Selection>();
    }

    void Update()
    {
        ActualiseShipPos();

        if (moving && Input.GetMouseButton(0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int clickPos = walkableTilemap.WorldToCell(mouseWorldPos);
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, mask);
            if (hit.collider.tag == "walkable" && moving == true && selection.ship.GetComponent<Stats>().moved == false)
            {
                for (int i = 0; i < walkable.Count; i++)
                {
                    if (clickPos == walkable[i])
                    {
                        selection.ship.GetComponent<AIDestinationSetter>().target = Instantiate(destination, walkableTilemap.GetCellCenterWorld(clickPos), Quaternion.identity).transform;
                        Destroy(GameObject.FindGameObjectWithTag("Destination"), 0.2f);
                        selection.ship.GetComponent<Stats>().moved = true;
                        moving = false;
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
                moving = false;
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
             if (WalkableCheck(vec))
             {
                tiles.Add(vec);
             }
        }
        return tiles;
    }


    public bool WalkableCheck(Vector3Int tile)
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
        foreach (var tile in walkable)
        {
            walkableTilemap.SetTile(tile,unselectedTile);
        }
        walkable.Clear();
    }
    
    void ColorWalkable()
    {
        foreach (var tile in walkable)
        {
            walkableTilemap.SetTile(tile,selectedTile);
        }
    }

    void ActualiseShipPos()
    {
        allShipsPos.Clear();
        
        for (int i = 0; i < allShips.Count; i++)
        {
            Vector3Int pos = Vector3Int.FloorToInt(allShips[i].transform.position);
            allShipsPos.Add(pos);
        }
    }


    public void Moving()
    {
        if ( selection.selected == true && moving == false)
        {
            ResetTilemap();
            if (selection.ship.GetComponent<Stats>().moved == false)
            {
                startTile = walkableTilemap.WorldToCell(selection.ship.transform.position);
                walkable = GetWalkableTiles(movePoints, startTile);
                ColorWalkable();
                moving = true;
                selection.choicePanel.SetActive(false);
            }
        }
    }
}