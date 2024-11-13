using UnityEngine;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{

    private int currentPlayer = 0;
    public GameObject container;
    public GameObject map;
    public Transform grid;
    public GameObject Player1, Player2, Player3, Player4, Player5, Player6, Player7, Player8, Player9, Player10;
    public int size = 5;
    public void EndTurn()
    {
        if (Global.players.Count == 0)
        {
            Debug.LogError("No hay jugadores en la lista. No se puede terminar el turno.");
            return;
        }

        Debug.Log($"Terminando turno. Jugador actual: {currentPlayer}, Total de jugadores: {Global.players.Count}");

        Global.players[currentPlayer][0].SetActive(false);
        currentPlayer = (currentPlayer + 1) % Global.players.Count;

        Debug.Log($"Nuevo jugador activo: {currentPlayer}");

        Global.players[currentPlayer][0].SetActive(true);
        
        StartTurn();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitPlayers();
        Container1 container0 = container.GetComponent<Container1>();
        container0.playersC = new List<GameObject[]>();
        container0.Init(size,Player2,Player1,Player3,Player4);
        Global.players = container0.playersC;
        MazeMap map0 = map.GetComponent<MazeMap>();
        map0.UpdateUI(grid);
        Debug.Log($"el count es {Global.players.Count}");
        ActivePlayer();
        StartTurn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartTurn()
    {
        //ActivePlayer();
        ChangeCamera();
        MovPlayer1 player0 = Global.players[currentPlayer][0].GetComponent<MovPlayer1>();
        player0.TakeTurn();
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


}
