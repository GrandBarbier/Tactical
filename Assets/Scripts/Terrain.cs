using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Script.Système_de_Tours;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Terrain : MonoBehaviour
{
    public Tilemap terrainNebul;
    public Tilemap terrainAster;
    
    public List<GameObject> allShips;
    public List<Vector3Int> allShipsPos;

    public int malusNebul;
    public int malusAster;
    
    private void Update()
    {
        allShips = allShips.Union(GameObject.FindGameObjectsWithTag("player1")).ToList();
        allShips = allShips.Union(GameObject.FindGameObjectsWithTag("player2")).ToList();
        
        ActualiseShipPos();
        TerrainCheck();
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
    
    public void TerrainCheck()
    {
        for (int i = 0; i < allShipsPos.Count; i++)
        {
            if (terrainNebul.GetTile(allShipsPos[i]) != null)
            {
                EffectNebul(allShips[i]);
            }
            else if (terrainAster.GetTile(allShipsPos[i]) != null)
            {
                EffectAster(allShips[i]);
            }
            else
            {
                Redo(allShips[i]);
            }
        }
    }

    public void EffectNebul(GameObject ship)
    {
        ship.GetComponent<Stats>().range = ship.GetComponent<Stats>().ship.portée - malusNebul;
    }

    public void EffectAster(GameObject ship)
    {
        ship.GetComponent<Stats>().movePoints = ship.GetComponent<Stats>().ship.mvt - malusAster;
    }

    public void Redo(GameObject ship)
    {
        ship.GetComponent<Stats>().range = ship.GetComponent<Stats>().ship.portée;
        ship.GetComponent<Stats>().movePoints = ship.GetComponent<Stats>().ship.mvt;
    }
}
