using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public Cube cube;
    public TextMeshProUGUI timer;
    public Toggle timerToggle;

    private float time; 
  

    void Awake()
    {
        //GameState.CubeSize = 3;
        InitGame();
    }

    void InitGame()
    {
        GameState.TimerEnabled = true;
        timerToggle.isOn = GameState.TimerEnabled;
        timer.enabled = GameState.TimerEnabled;
        cube = Instantiate(cube) as Cube;
        cube.GenerateCube();
        cube.ScrambleCube();
        GameState.InPlay = true;
        GameState.TimerEnabled = true;
    }
    
    void Update()
    {
        if (GameState.InPlay)
        {
            time += Time.deltaTime;
            GameState.CurrentTime = time;
            if (GameState.TimerEnabled)
            {
                string min = Mathf.FloorToInt(time / 60).ToString("00");
                string sec = Mathf.FloorToInt(time % 60).ToString("00");
                timer.text = min + ":" + sec;
            }
        }
    }
}
