using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovementsTilemap : MonoBehaviour
{
    public Tilemap walkableTilemap;
    public Tilemap obstacleTilemap;
    public Vector3Int actualTile;
    public TileBase selectedTile;
    public TileBase unselectedTile;
    
    public LayerMask mask;

    public int movePoints;
    public List<Vector3Int> walkableTiles;
    
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, mask);
            if (hit.collider != null)
            {
                if (hit.collider.tag == "Player")
                {
                    ResetTilemap();
                    Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector3Int clickPos = walkableTilemap.WorldToCell(mouseWorldPos);
                    actualTile = walkableTilemap.WorldToCell(clickPos);
                    GetWalkableTiles(movePoints, actualTile);
                    ColorWalkable();
                }
                else
                {
                    ResetTilemap();
                }
            }
        }
    }
    
    
    public List<Vector3Int> GetWalkableTiles(int range, Vector3Int start)
    {
        if (range >= 1)
        {
            List<Vector3Int> walkable= new List<Vector3Int>();
            walkable.Add(start);
            for (int i = 0; i < range; i++)
            {
                foreach (var tile in walkable)
                {
                    walkable= walkable.Union(GetAdjacentTiles(tile)).ToList();
                    walkableTiles = walkableTiles.Union(GetAdjacentTiles(tile)).ToList();
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
        if (obstacleTilemap.GetTile(tile) == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    
    void ResetTilemap()
    {
        walkableTilemap.SetTile(actualTile,unselectedTile);
        foreach (var tile in walkableTiles)
        {
            walkableTilemap.SetTile(tile,unselectedTile);
        }
        walkableTiles.Clear();
    }

    void ColorWalkable()
    {
        foreach (var tile in walkableTiles)
        {
            walkableTilemap.SetTile(tile,selectedTile);
        }
    }
}
