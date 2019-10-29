using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameState 
{
    public static int[] Cubies { get; set; }
    public static string[] Moves { get; set; }
    public static int CubeSize { get; set; }
    public static bool TimerEnabled { get; set; }
    public static bool InPlay { get; set; }
    public static float CurrentTime { get; set; }
}
