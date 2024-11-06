using UnityEngine;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

public class GameManager : MonoBehaviour
{

    private int currentPlayer = 0;
    public GameObject container;
    public int size = 5;
    public void EndTurn()
    {
        if (Global.players.Count == 0)
        {
            Debug.LogError("No hay jugadores en la lista. No se puede terminar el turno.");
            return;
        }

        Debug.Log($"Terminando turno. Jugador actual: {currentPlayer}, Total de jugadores: {Global.players.Count}");

        Global.players[currentPlayer].SetActive(false);
        currentPlayer = (currentPlayer + 1) % Global.players.Count;

        Debug.Log($"Nuevo jugador activo: {currentPlayer}");

        Global.players[currentPlayer].SetActive(true);
        
        StartTurn();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Container1 container0 = container.GetComponent<Container1>();
        container0.playersC = new List<GameObject>();
        container0.Init(size);
        Global.players = container0.playersC;
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
        MovPlayer1 player0 = Global.players[currentPlayer].GetComponent<MovPlayer1>();
        player0.TakeTurn();
    }
    void ChangeCamera()
    {
        foreach (var player in Global.players)
        {
            MovPlayer1 player0 = player.GetComponent<MovPlayer1>();
            player0.DeactivateCamera();
        }
        MovPlayer1 player1 = Global.players[currentPlayer].GetComponent<MovPlayer1>();
        player1.ActivateCamera();
        //Camera.main.transform.position = Global.players[currentPlayer].transform.position + new Vector3(0,2,-6);
        //Camera.main.transform.LookAt(Global.players[currentPlayer].transform.position);
    }
    
    void ActivePlayer()
    {
        foreach (var player in Global.players)
            {
                player.SetActive(false); 
            }
        if (Global.players.Count > 0)
        {
            Global.players[currentPlayer].SetActive(true); 
        }
    }


}
