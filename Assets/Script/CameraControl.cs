using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float zoomSize;
    
    //recuperation de la variable size de la camera qui gere le niveau de zoom 
    void Start()
    {
        zoomSize = 5;
    }

    void Update()
    {
        //deplacement de la camera dans la direction indique grace aux touches ZQSD
        if (Input.GetKey(KeyCode.Z))
        {
            transform.position += new Vector3(0, 0.1f, 0);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, -0.1f, 0);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            transform.position += new Vector3(-0.1f, 0,0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(0.1f, 0,0);
        }
        //gestion du niveau de zoom avec la molette de souris
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            zoomSize += 0.2f;
        }        
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            zoomSize -= 0.2f;
        }
        GetComponent<Camera>().orthographicSize = zoomSize;

    }
}
