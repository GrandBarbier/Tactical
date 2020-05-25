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

    public bool paused;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape")) //le jeu s'arrète, les boutons du menu pause s'affiche
        {
            paused = !paused;   
        }
        if (paused == true)
        {
            Debug.Log("PAUSE");
            Restart.SetActive(true);
            Resume.SetActive(true);
            QuitB.SetActive(true);
            Time.timeScale = 0;
        }
        if (paused == false)
        {
            Debug.Log("PAUSE");
            Restart.SetActive(false);
            Resume.SetActive(false);
            QuitB.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void ResumeGame()
    {
        Debug.Log("RESUME"); //le jeu reprend son cours, les boutons disparaissent de l'écran
        Restart.SetActive(false);
        Resume.SetActive(false);
        QuitB.SetActive(false);
        GameplayManager.SetActive(true);

        Time.timeScale = 1;
       
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;

    }
    public void Quit()
    {
        Application.Quit(); //quitter l'application
        Debug.Log("Quit");
        
    }

    public void Menu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //retour au menu (scene précedente)
    }
}
