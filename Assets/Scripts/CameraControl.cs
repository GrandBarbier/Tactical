using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float distance;
    public float distanceMax;
    public float distanceMin;
    
    private float rightBound;
    private float leftBound;
    private float topBound;
    private float bottomBound;
    private Vector3 pos;
    private Transform target;
    private SpriteRenderer spriteBounds;
    
    //recuperation de la variable size de la camera qui gere le niveau de zoom 
    void Start()
    {
        distance = 5;

        float vertExtent = Camera.main.orthographicSize;  
        float horzExtent = vertExtent * Screen.width / Screen.height;
        spriteBounds = GameObject.Find("BackgroundPs").GetComponentInChildren<SpriteRenderer>();
        target = GameObject.FindWithTag("MainCamera").transform;
        leftBound = (float)(horzExtent - spriteBounds.sprite.bounds.size.x / 2.0f);
        rightBound = (float)(spriteBounds.sprite.bounds.size.x / 2.0f - horzExtent);
        bottomBound = (float)(vertExtent - spriteBounds.sprite.bounds.size.y / 2.0f);
        topBound = (float)(spriteBounds.sprite.bounds.size.y  / 2.0f - vertExtent);
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
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && distance < distanceMax)
        {
            distance += 0.2f;
        }        
        else if (Input.GetAxis("Mouse ScrollWheel") > 0  && distance > distanceMin)
        {
            distance -= 0.2f;
        }
        GetComponent<Camera>().orthographicSize = distance;

        var pos = new Vector3(target.position.x, target.position.y, transform.position.z);
        pos.x = Mathf.Clamp(pos.x, leftBound, rightBound);
        pos.y = Mathf.Clamp(pos.y, bottomBound, topBound);
        transform.position = pos;
    }
}
