using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using UnityEngine.UI;

public class Station : MonoBehaviour
{
    public GameObject stationUI;
    public GameObject vaisseau;

    public Tilemap walkableTilemap;
    public Tilemap obstacleTilemap;

    public Vector3Int startTile;

    public TileBase selectedTile;
    public TileBase unselectedTile;

    public LayerMask mask;

    [TagSelector]
    public string TagFilterAlly = "";

    public List<Vector3Int> selectable;

    public List<GameObject> allShips;
    public List<Vector3Int> allShipsPos;

    public GameObject chosen;

    public Selection selection;

    public bool spawn;
    public bool turn;
    public bool usable;
    public bool spawned;

    public Vector3Int clickPos;

    public List<GameObject> allyStation;
    public int limit;
    public int baseCount;

    public List<GameObject> allyShips;

    public StatVaisseau actualShip;
    public Sprite actualSprite;

    public int money;
    public int nbStation;
    public Text textM;
    public int price;

    public float cd;
    public bool cdActif;

    public Text feedback;

   
    void Start()
    {
        cd = 5.0f;

        stationUI.SetActive(false);
        selection = gameObject.GetComponent<Selection>();
        feedback.gameObject.SetActive(false);
    }

    
    void Update()
    {
        if (cdActif == true)
        {
            cd = cd - Time.deltaTime;
        }
        if (cd <= 0)
        {
            feedback.gameObject.SetActive(false);
            cdActif = false;
            cd = 5.0f;
        }

        textM.text = money.ToString();

        allyStation = allyStation.Union(GameObject.FindGameObjectsWithTag(TagFilterAlly)).ToList();
        allShips = selection.allShips;
        allyShips = selection.allyShips;
        ActualiseShipPos();

        nbStation = allyStation.Count;
        
        limit = baseCount + allyStation.Count;
        if (allyShips.Count < limit)
        {
            usable = true;
        }
        else
        {
            usable = false;
        }
        
        if (Input.GetMouseButtonDown(0) && selection.selected == false && turn == true && usable == true && spawned == false)
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPos = walkableTilemap.WorldToCell(mouseWorldPos);
            
            if (spawn == false)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, mask);
                if (hit.collider != null)
                {
                    if(hit.collider.tag ==TagFilterAlly)
                    {
                        chosen = hit.collider.gameObject;
                        stationUI.SetActive(true);
                    } 
                }
            }
            else
            {
                for (int i = 0; i < selectable.Count; i++)
                {
                    if (clickPos == selectable[i])
                    {
                        for (int j = 0; j < allShipsPos.Count; j++)
                        {
                            if (clickPos != allShipsPos[j])
                            {
                                SpawnShip(actualShip);
                                actualShip = null;
                                break; 
                            }
                        }
                    }
                }
            }
        }
    }

    
    public void SpawnShip(StatVaisseau ship)
    {
        vaisseau.GetComponent<Stats>().ship = ship;
        
        Instantiate(vaisseau, walkableTilemap.GetCellCenterWorld(clickPos), Quaternion.identity);
        stationUI.SetActive(false);
        ResetTilemap();
        spawn = false;
        spawned = true;
    }

    public void Exit()
    {
        stationUI.SetActive(false);
        chosen = null;
    }

    public void ResetTilemap()
    {
        foreach (var tile in selectable)
        {
            walkableTilemap.SetTile(tile, unselectedTile);
        }
        selectable.Clear();
    }

    public void Spawning(StatVaisseau ship)
    {
        actualShip = ship;
        price = ship.prix;
        if (money >= price)
        {
            
            spawn = true;
            ResetTilemap();
            startTile = walkableTilemap.WorldToCell(chosen.transform.position);
            selectable = GetWalkableTiles(1, startTile);
            ColorWalkable();
            money = money - price;
        }
        else
        {
            cdActif = true;
            feedback.gameObject.SetActive(true);
            feedback.text = "Not Enough Mineral";
            
        }
        stationUI.SetActive(false); 
        
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
                    walkable = walkable.Union(GetAdjacentTiles(tile)).ToList();
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

    void ColorWalkable()
    {
        foreach (var tile in selectable)
        {
            walkableTilemap.SetTile(tile, selectedTile);
        }
    }

    public List<Vector3Int> GetAdjacentTiles(Vector3Int tile)
    {
        List<Vector3Int> testTiles = new List<Vector3Int>();
        testTiles.Add(tile + Vector3Int.up);
        testTiles.Add(tile + Vector3Int.down);
        testTiles.Add(tile + Vector3Int.left);
        testTiles.Add(tile + Vector3Int.right);
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

