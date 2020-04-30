using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovementsTilemap : MonoBehaviour
{
    public Tilemap walkableTilemap;
    public Tilemap obstacleTilemap;
    
    public Vector3Int startTile;

    public GameObject ship;

    public TileBase selectedTile;
    public TileBase unselectedTile;
    
    public LayerMask mask;

    [TagSelector]
    public string TagFilterAlly = "";
    
    [TagSelector]
    public string TagFilterEnemy = "";
    
    public int movePoints;
    
    public List<Vector3Int> walkable;
    
    public List<GameObject> allShips;
    public List<GameObject> allyShips;
    public List<Vector3Int> allShipsPos;


    public bool selected;

    private void Start()
    {
        allyShips = allyShips.Union(GameObject.FindGameObjectsWithTag(TagFilterAlly)).ToList();
        allShips = allShips.Union(GameObject.FindGameObjectsWithTag(TagFilterAlly)).ToList();
        allShips = allShips.Union(GameObject.FindGameObjectsWithTag(TagFilterEnemy)).ToList();
       
        ActualiseShipPos();
    }

    void Update()
    {
        allyShips = allyShips.Union(GameObject.FindGameObjectsWithTag(TagFilterAlly)).ToList();
        allShips = allShips.Union(GameObject.FindGameObjectsWithTag(TagFilterAlly)).ToList();
        allShips = allShips.Union(GameObject.FindGameObjectsWithTag(TagFilterEnemy)).ToList();
        ActualiseShipPos();
        
        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < allShips.Count; i++)
            {
                Destroy(allShips[i].GetComponent<BoxCollider2D>());
                allShips[i].AddComponent<BoxCollider2D>();
            }
            
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, mask);
            if (hit.collider != null)
            {
                Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int clickPos = walkableTilemap.WorldToCell(mouseWorldPos);
                if (hit.collider.tag == TagFilterAlly && selected == false)
                {
                    ResetTilemap();
                    ship = hit.collider.gameObject;
                    selected = true;
                    if (ship.GetComponent<Stats>().moved == false)
                    {
                        startTile = walkableTilemap.WorldToCell(clickPos);
                        walkable = GetWalkableTiles(movePoints, startTile);
                        ColorWalkable();
                    }
                }
                else if (hit.collider.tag == "walkable" && selected == true && ship.GetComponent<Stats>().moved == false)
                {
                    for (int i = 0; i < walkable.Count; i++)
                    {
                        if (clickPos == walkable[i])
                        {
                            ship.transform.position = walkableTilemap.GetCellCenterWorld(clickPos);
                            ship.GetComponent<Stats>().moved = true;
                        }
                    }
                    selected = false;
                    ResetTilemap();
                    ship = null;
                }
                else if (selected == true)
                {
                    selected = false;
                    ResetTilemap();
                    ship = null;
                }
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
    
    void ResetTilemap()
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
}