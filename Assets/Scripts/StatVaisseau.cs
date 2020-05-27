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

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
