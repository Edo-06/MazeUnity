using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System;


public class GameManager : MonoBehaviour
{

    private int currentPlayer = 0;
    public GameObject container;
    public GameObject map;
    public Transform grid;
    public GameObject Player1, Player2, Player3, Player4, Player5, Player6, Player7, Player8, Player9, Player10;
    public int size = 5;
    public float turnDuration = 20f;
    public TMP_Text timerText;
    public GameObject menuPanel;
    //private bool isInit = false;
    
    

    void StartTurn()
    {
        //ActivePlayer();
        ChangeCamera();
        MovPlayer1 player0 = Global.players[currentPlayer][0].GetComponent<MovPlayer1>();
        player0.TakeTurn();
        player0.lastPosition = player0.transform.position;
        player0.total = 0;
        UpdateHealthBars();
        //StartCoroutine(TurnTimer());
    }
    public void EndTurn()
    {
        turnDuration = 20f;
        menuPanel.SetActive(false);
        Global.isPaused = false;
        if (Global.players.Count == 0)
        {
            Debug.LogError("No hay jugadores en la lista");
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
        menuPanel.SetActive(false);
        InitPlayers();
        Container1 container0 = container.GetComponent<Container1>();
        //container1 = container0;
        container0.playersC = new List<GameObject[]>();
        container0.Init(size,Global.players);
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
        if(turnDuration > 0 && !menuPanel.activeSelf && !Global.isPaused)
        {
            turnDuration -= Time.deltaTime;
            timerText.text = Mathf.Ceil(turnDuration).ToString();
        }
        else
        {
            Global.isPaused = true;
            menuPanel.SetActive(true);
        }
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

    /*private IEnumerator TurnTimer()
    {
        float timeRemaining = turnDuration;
        while (timeRemaining > 0)
        {
            Debug.Log($"Tiempo restante: {timeRemaining:F1}");
            timerText.text = $"Tiempo restante: {timeRemaining:F1}";
            yield return new WaitForSeconds(1f);
            timeRemaining -= 1f;
        }
        //yield return new WaitForSeconds(turnDuration);
        timerText.text = "Tiempo restante: 0";
        menuPanel.SetActive(true);
    }*/
    void UpdateHealthBars()
    {
        for (int i = 0; i < Global.players.Count; i++)
        {
            GameObject[] playerPair = Global.players[i];
            foreach (GameObject player in playerPair)
            {
                Health health = player.GetComponent<Health>();
                if (health != null)
                {
                    //health.healthSlider .SetActive(i == currentPlayer);
                }
            }
        }
    }
}
