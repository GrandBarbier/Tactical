using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CaptureTilemap : MonoBehaviour
{
    public Tilemap walkableTilemap;

    public Vector3Int startTile;
    
    public TileBase selectedTile;
    public TileBase unselectedTile;
    
    public LayerMask mask;
    
    public List<Vector3Int> targetable;
    
    
    public List<GameObject> neutralStation;
    public List<Vector3Int> stationPos;

    public Selection selection;

    public bool capturing;
    

    private void Start()
    {
        selection = gameObject.GetComponent<Selection>();
    }

    void Update()
    {
        neutralStation.Clear();
        neutralStation = neutralStation.Union(GameObject.FindGameObjectsWithTag("Station")).ToList();

        ActualiseShipPos();

        if (capturing && Input.GetMouseButton(0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int clickPos = walkableTilemap.WorldToCell(mouseWorldPos);
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, mask);
            if (hit.collider != null)
            {
                if (selection.ship.GetComponent<Stats>().attacked == false)
                {
                    for (int i = 0; i < targetable.Count; i++)
                    {
                        if (clickPos == targetable[i])
                        {
                            for (int j = 0; j < stationPos.Count; j++)
                            {
                                if (clickPos == stationPos[j])
                                {
                                    if (hit.collider.gameObject.CompareTag("Station"))
                                    {
                                        selection.ship.GetComponent<Stats>().attacked = true; 
                                        hit.collider.gameObject.GetComponent<StationState>().Capture(gameObject);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            
            selection.deselected = true; 
            ResetTilemap(); 
            capturing = false;
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
        for (int i = 0; i < stationPos.Count; i++)
        {
            if (tile == stationPos[i])
            {
                return true;
                break;
            }
        }
        return false;
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
        for (int i = 0; i < neutralStation.Count; i++)
        {
            Vector3Int pos = Vector3Int.FloorToInt(neutralStation[i].transform.position);
            stationPos.Add(pos);
        }
    }


    public void Capturing()
    {
        if ( selection.selected == true && capturing == false)
        {
            ResetTilemap();
            if (selection.ship.GetComponent<Stats>().attacked == false)
            {
                startTile = walkableTilemap.WorldToCell(selection.ship.transform.position);
                targetable = GetWalkableTiles(1, startTile);
                ColorWalkable();
                capturing = true;
                selection.choicePanel.SetActive(false);
            }
        }
    }
}
