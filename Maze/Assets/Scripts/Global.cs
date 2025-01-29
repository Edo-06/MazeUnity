using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class Global
{
    public static List<GameObject[]> players;
    public static Maze maze;
    public static bool isPaused = false, onEndTurn = false, atack = false, healing;
    public static GameObject trapP, final;
    public static TMP_Text trapT, finalT;
    public static int currentPlayer, currentPlayerPoisoned, currentPlayerAtacked, currentPlayerCurse, currentPlayerMurdered, currentPlayerInmobilized, index = 0;
    public static List<int[]> allTheTraps = new List<int[]>();
    public static List<int[]> atTheGoal = new List<int[]>();
}
