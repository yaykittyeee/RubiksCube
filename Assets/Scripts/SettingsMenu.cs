using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public void OpenSettingsMenu()
    {
        Debug.Log("Open settings!!!");
    }

    public void UndoMove()
    {
        Debug.Log("UNDO!!!");
        // have a stack of Move objects in GameState
        // the Move object will have stuff axis, direction, level(0, 1, 2)
        // the Move object can also have its own undo function that initiates the undo
        // to undo a move pop a Move from the stack and call Move.Undo()
    }

    public void ToggleTimer()
    {
        GameState.TimerEnabled = !GameState.TimerEnabled;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
