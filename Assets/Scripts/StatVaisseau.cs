using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ship", menuName = "Ship")]
public class StatVaisseau : ScriptableObject
{
    public int prix;
    public int shield;
    public int health;
    public int mvt;
    public int dmgMin;
    public int dmgMax;
    public int portée;
    public Sprite sprite1;
    public Sprite sprite2;

    public int id;

    public Sprite dirigent1;
    public Sprite dirigent2;

    public AudioClip shootSound;
    public AudioClip explosionSound;
    public AudioClip hitSound;

    public AudioSource audioOnShips;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
