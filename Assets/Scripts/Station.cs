using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using TMPro;
using UnityEngine.UI;
using Debug = System.Diagnostics.Debug;


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

    [TagSelector]
    public string TagFilterCoreStation = "";

    public List<Vector3Int> selectable;

    public List<GameObject> allShips;
    public List<Vector3Int> allShipsPos;

    public GameObject chosen;

    public Selection selection;

    public bool spawn;
    public bool turn;
    public bool usable;

    public Vector3Int clickPos;

    public List<GameObject> allyStation;
    public int limit;
    public int baseCount;

    public List<GameObject> allyShips;

    public StatVaisseau actualShip;

    public Text textLimit;
    public Text textIncome;
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
        allyStation = allyStation.Union(GameObject.FindGameObjectsWithTag(TagFilterCoreStation)).ToList();
        allShips = selection.allShips;
        allyShips = selection.allyShips;
        ActualiseShipPos();
        
        nbStation = allyStation.Count;

        textIncome.text = "(+" + (((allyStation.Count - 1) * 500) + 2000) + ")";
        
        limit = baseCount + allyStation.Count;
        if (allyShips.Count < limit)
        {
            usable = true;
        }
        else
        {
            usable = false;
        }

        textLimit.text = "Max : " + allyShips.Count.ToString() + "/" + limit;
        
        if (Input.GetMouseButtonDown(0) && turn == true && usable == true && selection.selected == false)
        {
            if (spawn == false)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),
                    Vector2.zero, Mathf.Infinity, mask);
                if (hit.collider != null)
                {
                    if (hit.collider.tag == TagFilterAlly || hit.collider.tag == TagFilterCoreStation)
                    {
                        if (hit.collider.gameObject.GetComponent<StationState>().spawned == false)
                        {
                            chosen = hit.collider.gameObject;
                            stationUI.SetActive(true);
                        }
                    }
                }
            }
            else
            {
                Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                clickPos = walkableTilemap.WorldToCell(mouseWorldPos);
                
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
                                chosen = null;
                                break;
                            }
                        }
                    }
                }
                actualShip = null;
                ResetTilemap();
                chosen = null;
                spawn = false;
            }
        }
    }

    
    public void SpawnShip(StatVaisseau ship)
    {
        vaisseau.GetComponent<Stats>().ship = ship;
        vaisseau.GetComponent<Stats>().moved = true;
        vaisseau.GetComponent<Stats>().attacked = true;
        Instantiate(vaisseau, walkableTilemap.GetCellCenterWorld(clickPos), Quaternion.identity);
        money = money - price;
        stationUI.SetActive(false);
        ResetTilemap();
        spawn = false;
        chosen.GetComponent<StationState>().spawned = true;
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
        testTiles.Add(tile + Vector3Int.left);
        testTiles.Add(tile + Vector3Int.right);
        testTiles.Add(tile + Vector3Int.up);
        testTiles.Add(tile + Vector3Int.down);
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

