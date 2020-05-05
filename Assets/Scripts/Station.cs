using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    public GameObject stationUI;
    public int argent;
    public GameObject text;
    public GameObject vaisseau;
    // Start is called before the first frame update
    void Start()
    {
        stationUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity);

        if (hit.collider.tag == "StationP1" && Input.GetMouseButtonDown(0))
        {
            stationUI.SetActive(true);
        }

        argent = text.GetComponent<Money>().argent;
    }

    public void SpawnCroiseur()
    {
        
            Instantiate(vaisseau, new Vector3(0, 0, 0), Quaternion.identity);
        
    }

    public void Exit()
    {
        stationUI.SetActive(false);


    }
}
