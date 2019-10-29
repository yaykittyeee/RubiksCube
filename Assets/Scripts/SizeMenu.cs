using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SizeMenu : MonoBehaviour
{
    public void PlayGame(int size)
    {
        GameState.CubeSize = size;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
