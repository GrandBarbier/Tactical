using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV : MonoBehaviour
{
    public float rayDistance;
    public LayerMask mask;
    public float angle;
    public uint nbRaycast;
    void Start()
    {
        
    }

   
    void Update()
    {
        RaycastHit2D hit;
        
        
        for (int i = 0; i < nbRaycast; i++)
        {

           hit = Physics2D.Raycast(transform.position,
                Quaternion.AngleAxis(angle / nbRaycast * i, Vector3.back) * Vector3.up * rayDistance, mask);
           if (hit != null)
           {
               Vector2 point = hit.point;
               Debug.Log(hit.collider.name);
               Debug.DrawLine(transform.position,point);
           }
           else
           {
               Debug.DrawRay(transform.position, Quaternion.AngleAxis(angle/nbRaycast*i, Vector3.back) * Vector3.up * rayDistance, Color.red);
           }
        }
    }
}
