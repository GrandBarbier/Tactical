using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class VictoryManager : MonoBehaviour
{
    public GameObject coreStationJ1;
    public GameObject coreStationJ2;

    public GameObject VictoryJ1;
    public GameObject VictoryJ2;
    public GameObject panel;
    public GameObject Restart;
    public GameObject GameManager;

    public GameObject Resume;
    public GameObject QuitB;
    public GameObject tuto;
    public GameObject abandon;
    public GameObject suite;

    public GameObject InterfaceH;
    // Start is called before the first frame update
    void Start()
    {
        coreStationJ2 = GameObject.FindWithTag("CoreStationP2");
        coreStationJ1 = GameObject.FindWithTag("CoreStationP1");
    }

    // Update is called once per frame
    void Update()
    {
        if (coreStationJ1.GetComponent<StationState>().health <= 0)
        {
            victoryJ2();
        }

        if (coreStationJ2.GetComponent<StationState>().health <= 0)
        {
            victoryJ1();
        }        
    }

    public void victoryJ2()
    {
        GameManager.GetComponent<MenuPause>().enabled = false;
        panel.SetActive(true);
        VictoryJ2.SetActive(true);
        coreStationJ1.GetComponent<Animator>().Play("CoreExplosion");
        Destroy(coreStationJ1, 1.0f);
        Restart.SetActive(false);
        StartCoroutine(Victory());
    }

    public void victoryJ1()
    {
        GameManager.GetComponent<MenuPause>().enabled = false;
        panel.SetActive(true);
        VictoryJ1.SetActive(true);
        Restart.SetActive(false);
        coreStationJ1.GetComponent<Animator>().Play("CoreExplosion");
        Destroy(coreStationJ2, 1.0f);
        StartCoroutine(Victory());
    }

    public void giveUp() 
    {
        if (InterfaceH == true)
        {
            GameManager.GetComponent<MenuPause>().enabled = false;
            panel.SetActive(true);
            VictoryJ2.SetActive(true);
            coreStationJ1.GetComponent<Animator>().Play("CoreExplosion");
            Destroy(coreStationJ1, 1.0f);
            Restart.SetActive(false);
            StartCoroutine(Victory());

            tuto.SetActive(false);
            Resume.SetActive(false);
            QuitB.SetActive(false);
            abandon.SetActive(false);
            suite.SetActive(true);
        }
        else
        {
            GameManager.GetComponent<MenuPause>().enabled = false;
            panel.SetActive(true);
            VictoryJ1.SetActive(true);
            Restart.SetActive(false);
            coreStationJ1.GetComponent<Animator>().Play("CoreExplosion");
            Destroy(coreStationJ2, 1.0f);
            StartCoroutine(Victory());

            tuto.SetActive(false);
            Resume.SetActive(false);
            QuitB.SetActive(false);
            abandon.SetActive(false);
            suite.SetActive(true);

        }
    }

    IEnumerator Victory()
    {
        yield return new WaitForSeconds(2);
        Time.timeScale = 0;
    }

    public void Suite()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
