using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    public List<GameObject> Nbstation = new List<GameObject>();
    public int argent;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject station in GameObject.FindGameObjectsWithTag("StationP1"))
        {
            Nbstation.Add(station);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = argent.ToString();
    }

    public void LelioEstLong()
    {
        argent = argent + Nbstation.Count * 1500;
    }
}
