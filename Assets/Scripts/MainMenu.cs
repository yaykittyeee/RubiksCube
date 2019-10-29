using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void ContinueGame()
    {
        Debug.Log("Continue game!!!");
        //load GameState from json
        //Call a different constructor for GameManager???
        //or set flag in GameState as continue and modify InitGame in GameManager
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit game!!!");
        Application.Quit();
    }
}
