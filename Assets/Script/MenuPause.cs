using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    public GameObject MenuButton;
    public GameObject Resume;

    public GameObject Option;
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
            MenuButton.SetActive(true);
            Resume.SetActive(true);
            Time.timeScale = 0;
            Option.SetActive(true);
        }
    }
    public void ResumeGame()
    {
        Debug.Log("RESUME"); //le jeu reprend son cours, les boutons disparaissent de l'écran
        MenuButton.SetActive(false);
        Resume.SetActive(false);
        Time.timeScale = 0;
        Option.SetActive(false);
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
