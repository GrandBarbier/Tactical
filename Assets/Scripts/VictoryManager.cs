using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryManager : MonoBehaviour
{
    public GameObject coreStationJ1;
    public GameObject coreStationJ2;

    public GameObject VictoryJ1;
    public GameObject VictoryJ2;
    public GameObject panel;
    public GameObject Restart;
    public GameObject GameManager;

    public AudioSource audio;

    public AudioClip exploSound;
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
            audio.clip = exploSound;
            audio.Play();
            GameManager.GetComponent<MenuPause>().enabled = false;
            panel.SetActive(true);
            VictoryJ2.SetActive(true);
            coreStationJ1.GetComponent<Animator>().Play("CoreExplosion");
            Destroy(coreStationJ1, 1.0f);
            Restart.SetActive(true);
            StartCoroutine(Victory());
        }


        if (coreStationJ2.GetComponent<StationState>().health <= 0)
        {
            audio.clip = exploSound;
            audio.Play();
            GameManager.GetComponent<MenuPause>().enabled = false;
            panel.SetActive(true);
            VictoryJ1.SetActive(true);
            Restart.SetActive(true);
            coreStationJ1.GetComponent<Animator>().Play("CoreExplosion");
            Destroy(coreStationJ2, 1.0f);
            StartCoroutine(Victory());
            


        }

        IEnumerator Victory()
        {
            yield return new WaitForSeconds(2);
            Time.timeScale = 0;
        }
    }
}
