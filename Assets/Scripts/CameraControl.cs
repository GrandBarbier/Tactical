using System;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float distance;
    public float distanceMax;
    public float distanceMin;
    public float panSpeed = 20f;
    public Vector2 panLimit;

    //recuperation de la variable size de la camera qui gere le niveau de zoom 
    void Start()
    {
        distance = 5;
    }

    void Update()
    {
        Vector2 pos = transform.position;
        //deplacement de la camera dans la direction indique grace aux touches ZQSD
        if (Input.GetKey(KeyCode.Z))
        {
            pos.y += panSpeed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            pos.y -= panSpeed;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            pos.x -= panSpeed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            pos.x += panSpeed;
        }
        //gestion du niveau de zoom avec la molette de souris
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && distance < distanceMax)
        {
            distance += 0.2f;
        }        
        else if (Input.GetAxis("Mouse ScrollWheel") > 0  && distance > distanceMin)
        {
            distance -= 0.2f;
        }
        GetComponent<Camera>().orthographicSize = distance;
        
        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.y = Mathf.Clamp(pos.y, -panLimit.y, panLimit.y);
        transform.position = new Vector3(pos.x, pos.y,-10f);
    }


}
