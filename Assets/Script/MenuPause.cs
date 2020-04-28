using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    public GameObject Resume;
    public GameObject Restart;
    public GameObject QuitB;
    public GameObject Ship;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape")) //le jeu s'arrète, les boutons du menu pause s'affiche
        {
            Debug.Log("PAUSE");
            Restart.SetActive(true);
            Resume.SetActive(true);
            QuitB.SetActive(true);
            Ship.SetActive(false);
            Time.timeScale = 0;
            
        }
    }
    public void ResumeGame()
    {
        Debug.Log("RESUME"); //le jeu reprend son cours, les boutons disparaissent de l'écran
        Restart.SetActive(false);
        Resume.SetActive(false);
        QuitB.SetActive(false);
        Ship.SetActive(true);

        Time.timeScale = 1;
       
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game"); //restart
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
