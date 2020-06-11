using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    public GameObject Resume;
    public GameObject Restart;
    public GameObject QuitB;
    public GameObject GameplayManager;
    public GameObject panel;
    public GameObject tuto;

    public GameObject choicePanel1;
    public GameObject choicePanel2;
    public GameObject stationPanel1;
    public GameObject stationPanel2;
    public GameObject stat;

    public AudioSource audio;
    public AudioClip pauseSound;
    public AudioClip uiSound;

    public bool paused;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape")) //le jeu s'arrète, les boutons du menu pause s'affiche
        {
            paused = !paused;
            audio.clip = pauseSound;
            audio.Play();
        }
        if (paused == true)
        {
            Debug.Log("PAUSE");
            Restart.SetActive(true);
            Resume.SetActive(true);
            QuitB.SetActive(true);
            stationPanel1.SetActive(false);
            stationPanel2.SetActive(false);
            choicePanel1.SetActive(false);
            choicePanel2.SetActive(false);
            stat.SetActive(false);
            panel.SetActive(true);
            tuto.SetActive(true);
            Time.timeScale = 0;
        }
        if (paused == false)
        {
            
            Restart.SetActive(false);
            Resume.SetActive(false);
            QuitB.SetActive(false);
            tuto.SetActive(false);
            stat.SetActive(true);
            panel.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void ResumeGame()
    {
        audio.clip = uiSound;
        audio.Play();
        paused = false;
    }

    public void RestartGame()
    {
        audio.clip = uiSound;
        audio.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;

    }
    public void Quit()
    {
        audio.clip = uiSound;
        audio.Play();
        Application.Quit(); //quitter l'application
        Debug.Log("Quit");
        
    }

    public void Menu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //retour au menu (scene précedente)
    }
}
