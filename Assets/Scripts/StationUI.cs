using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class StationUI : MonoBehaviour
{
    public GameObject Image;
    public TextMeshProUGUI text;

    public List<GameObject> Buy;




    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InfoShip(StatVaisseau ship)
    {
        Image.GetComponent<Image>().sprite = ship.sprite1;
        text.text = "Price : " + ship.prix + "\n" + "HP : " + ship.health + "\n" + "Range : " + ship.portée + "\n" + "Movement : " + ship.mvt + "\n" + "Damage : " + ship.dmgMin + " to " + ship.dmgMax; 
    }

    public void InfoShip2(StatVaisseau ship)
    {
        Image.GetComponent<Image>().sprite = ship.sprite2;
        text.text = "Price : " + ship.prix + "\n" + "HP : " + ship.health + "\n" + "Range : " + ship.portée + "\n" + "Movement : " + ship.mvt + "\n" + "Damage : " + ship.dmgMin + " to " + ship.dmgMax;
    }

    public void Place(int place)
    {
        foreach (GameObject B in Buy)
        {
            B.SetActive(false);
        }
        Buy[place].SetActive(true);
    }
}
