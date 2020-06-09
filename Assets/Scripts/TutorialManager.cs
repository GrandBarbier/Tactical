using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject Page1;
    public GameObject Page2;

    public GameObject Resume;
    public GameObject Restart;
    public GameObject QuitB;
    public GameObject tuto;
    // Start is called before the first frame update
    void Start()
    {
        Page1.SetActive(false);
        Page2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void tutorialButton()
    {
        Page1.SetActive(true); Restart.SetActive(false);
        
    }

    public void Next()
    {
        Page2.SetActive(true);
        Page1.SetActive(false);
    }

    public void Prev()
    {
        Page2.SetActive(false);
        Page1.SetActive(true);
    }

    public void Exit()
    {
        Page2.SetActive(false);
        Page1.SetActive(false);
        
    }
}
