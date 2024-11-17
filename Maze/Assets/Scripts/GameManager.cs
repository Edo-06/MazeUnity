using UnityEngine;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using Unity.VisualScripting;
using System;

public class GameManager : MonoBehaviour
{

    private int currentPlayer = 0;
    public GameObject container;
    public GameObject map;
    public Transform grid;
    public GameObject Player1, Player2, Player3, Player4, Player5, Player6, Player7, Player8, Player9, Player10;
    public int size = 5;
    //private bool isInit = false;
    
    public void EndTurn()
    {
        if (Global.players.Count == 0)
        {
            Debug.LogError("No hay jugadores en la lista. No se puede terminar el turno.");
            return;
        }

        Debug.Log($"Terminando turno Jugador actual: {currentPlayer}, Total de jugadores: {Global.players.Count}");
        Global.players[currentPlayer][0].SetActive(false);
        currentPlayer = (currentPlayer + 1) % Global.players.Count;
        //MovPlayer1 player0 = Global.players[currentPlayer][0].GetComponent<MovPlayer1>();
        //player0.total = 0;

        Debug.Log($"Nuevo jugador activo: {currentPlayer}");

        Global.players[currentPlayer][0].SetActive(true);
        
        StartTurn();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitPlayers();
        Container1 container0 = container.GetComponent<Container1>();
        //container1 = container0;
        container0.playersC = new List<GameObject[]>();
        container0.Init(size,Player2,Player1,Player3,Player4);
        Global.players = container0.playersC;
        MazeMap map0 = map.GetComponent<MazeMap>();
        map0.UpdateUI(grid);
        Debug.Log($"el count es {Global.players.Count}");
        ActivePlayer();
        StartTurn();
        //isInit = true;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Global.players.Count > 0)
        {            MovPlayer1 current = Global.players[currentPlayer][0].GetComponent<MovPlayer1>();
            if (current != null){ 
            //Debug.Log(current.transform.position.z);
            List<int[]> boundary = BoundaryCells(current.transform.position.x, current.transform.position.z, 2);
        
        if (boundary == null || boundary.Count == 0)
        {
            Debug.LogWarning("BoundaryCells returned null or an empty list");
            return; 
        }

        for (int i = 0; i < boundary.Count; i++) // Cambié a boundary.Count (sin -1)
        {
                if (Math.Abs(current.transform.position.x - boundary[i][0]) < 0.5 && Math.Abs(current.transform.position.z - boundary[i][1]) < 0.5) // poner en un rango 
            {
                current.transform.position = new Vector3 (boundary[i][0], 0.4f, boundary[i][1]) ;
                EndTurn();
            }
        }   }
        }*/
    }

    void StartTurn()
    {
        //ActivePlayer();
        ChangeCamera();
        MovPlayer1 player0 = Global.players[currentPlayer][0].GetComponent<MovPlayer1>();
        player0.TakeTurn();
        player0.lastPosition = player0.transform.position;
        player0.total = 0;
    }
    void ChangeCamera()
    {
        foreach (var player in Global.players)
        {
            MovPlayer1 player0 = player[0].GetComponent<MovPlayer1>();
            MovPlayer1 player01 = player[1].GetComponent<MovPlayer1>();
            player0.DeactivateCamera();
            player01.DeactivateCamera();
        }
        MovPlayer1 player1 = Global.players[currentPlayer][0].GetComponent<MovPlayer1>();
        player1.ActivateCamera();
        //Camera.main.transform.position = Global.players[currentPlayer].transform.position + new Vector3(0,2,-6);
        //Camera.main.transform.LookAt(Global.players[currentPlayer].transform.position);
    }
    
    void ActivePlayer()
    {
        foreach (var player in Global.players)
            {
                player[0].SetActive(false);
                player[1].SetActive(false);
            }
        if (Global.players.Count > 0)
        {
            Global.players[currentPlayer][0].SetActive(true); 
        }
    }

    void InitPlayers()
    {
        InitPLayer(Player1, 100, 10, "s1");
        InitPLayer(Player2, 100, 10, "s2");
        InitPLayer(Player3, 100, 10, "s3");
        InitPLayer(Player4, 100, 10, "s4");
        InitPLayer(Player5, 100, 10, "s5");
        InitPLayer(Player6, 100, 10, "s6");
        InitPLayer(Player7, 100, 10, "s7");
        InitPLayer(Player8, 100, 10, "s8");
        InitPLayer(Player9, 100, 10, "s9");
        InitPLayer(Player10, 100, 10, "s10");

        
    }

    void InitPLayer(GameObject player, float health, float speed, string skill)
    {
        MovPlayer1 player1 = player.GetComponent<MovPlayer1>();
        player1.character = new Character(health, speed, skill);
       // Debug.Log(player1.character.skill);
    }

    public List<int[]> BoundaryCells(float rowFloat, float colFloat, float distance)
        {
            int row = (int)rowFloat;
            int col = (int)colFloat;


            List<int[]> neighbords = new List<int[]>();
            List<int[]> usedcells0 = new List<int[]>();
            Neighborhod(neighbords, usedcells0, row, col);
            List<int[]> boundary = Path(neighbords, usedcells0, distance);
            return boundary;
        }

    private void Neighborhod(List<int[]> neighbords, List<int[]> usedcells0, int row, int col)
    {   //Container1 container0 = container.GetComponent<Container1>();
        usedcells0.Add(new int[] {row, col});
        if (row >= 1 && Global.maze.mazee[row - 1, col].category == Category.floor &&  !usedcells0.Contains(new int[] { row - 1, col }))
            neighbords.Add(new int[] { row - 1, col });
        if (col >= 1 && Global.maze.mazee[row, col - 1].category == Category.floor  && !usedcells0.Contains(new int[] { row, col - 1 }))
            neighbords.Add(new int[] { row, col - 1 });
        if (row <= size - 2 && Global.maze.mazee[row + 1, col].category == Category.floor  && !usedcells0.Contains(new int[] { row + 1, col }))
            neighbords.Add(new int[] { row + 1, col });
        if (col <= size - 2 && Global.maze.mazee[row, col + 1].category == Category.floor   && !usedcells0.Contains(new int[] { row, col + 1 }))
            neighbords.Add(new int[] { row, col + 1 });
    }

    private List<int[]> Path(List<int[]> neighbords,List<int[]> usedcells0, float distance)
    {
        int x=0;
        int y=0;
        List<int[]> boundary = new List<int[]>();

        if(distance <= 0)
        {
            x = usedcells0[usedcells0.Count - 1][0];
            y = usedcells0[usedcells0.Count - 1][1];
            boundary.Add(new int[]{x, y});
        }

        while(distance > 0)
        {
            if(distance  > 1)
            {
                for (int i = 0; i < neighbords.Count; i++)
                {
                    Neighborhod(neighbords, usedcells0, neighbords[neighbords.Count-i-1][0], neighbords[neighbords.Count-i-1][1]);
                    neighbords.RemoveAt(i);
                    if (distance > 0)
                    {
                        distance--;
                        var result = Path(neighbords, usedcells0, distance); 
                        boundary.AddRange(result); 
                    }
                }
            }    
            else
            {
                for(int i = 0; i < neighbords.Count; i++)
                {
                    boundary.Add(neighbords[i]);
                }
                distance--;
            }
        }   
        
        
        return boundary; 
    }

void X ()
{
    Container1 container0 = container.GetComponent<Container1>();
    Debug.Log(Global.maze.mazee[1,1].category);
}

}
