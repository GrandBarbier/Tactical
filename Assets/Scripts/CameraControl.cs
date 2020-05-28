using System;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float distance;
    public float distanceMax;
    public float distanceMin;
    public float panSpeed;
    public Vector2 panLimit;
    public float panBorderThickness;

    //recuperation de la variable distance de la camera qui gere le niveau de zoom 
    void Start()
    {
        distance = 8;
    }

    void Update()
    {
        Vector2 pos = transform.position;
        //deplacement de la camera dans la direction indique grace aux touches ZQSD/flèche du clavier ou placement de souris au bord de l'écran
        if (Input.GetKey(KeyCode.Z) || Input.mousePosition.y >= Screen.height - panBorderThickness || Input.GetKey(KeyCode.UpArrow))
        {
            pos.y += panSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= panBorderThickness || Input.GetKey(KeyCode.DownArrow))
        {
            pos.y -= panSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Q) || Input.mousePosition.x <= panBorderThickness || Input.GetKey(KeyCode.LeftArrow))
        {
            pos.x -= panSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - panBorderThickness || Input.GetKey(KeyCode.RightArrow))
        {
            pos.x += panSpeed * Time.deltaTime;
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
        transform.position = new Vector3(pos.x, pos.y, -10f);
    }


}
