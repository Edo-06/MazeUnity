using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class Global
{
    public static List<GameObject[]> players;
    public static Maze maze;
    public static bool isPaused = false, onEndTurn = false;
    public static GameObject trapP;
    public static TMP_Text trapT;
    public static int currentPlayer, index = 0;
    public static List<int[]> allTheTraps = new List<int[]>();
}
