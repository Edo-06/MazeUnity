using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public class GameManager : MonoBehaviour
{
    private MovPlayer1 player;
    private float count = 5, poisonedTime = 10;
    private int currentPlayer = 0;
    public GameObject container;
    public GameObject map;
    public Transform grid;
    public GameObject Player1, Player2, Player3, Player4, Player5, Player6, Player7, Player8, Player9, Player10;
    public int size = 30;
    public TMP_Text timerText, trapText, abilityText, healthText, abilityTime, finalT, key, atackText;
    public GameObject menuPanel, deathPanel, trapPanel, selectCharacterPanel, exitPanel, final, atackP;
    public Slider healthBar, abilityCooldownBar,abilityActiveDurationBar;
    private Container1 container0;
    public Button abilityButton;
    private MazeMap map0 ;
    private float turnDuration;
    private int[] playerInfo, playerInfo1;
    private bool contains = false;
    private int index;
    public Button[] buttons;
    //private bool isInit = false;
    
    

    void StartTurn()
    {
        //ActivePlayer();
        ChangeCamera();
        Global.currentPlayer = currentPlayer;
        player = Global.players[currentPlayer][Global.index].GetComponent<MovPlayer1>();
        player.TakeTurn();
        player.lastPosition = player.transform.position;
        Debug.Log(player.character.health);
        turnDuration = player.character.turnDuration;
        if(player.character.AbilityIsActive())
        {
            player.character.currentActiveTime = 0f;
            if(player.character.ability == Abilities.trapDetector)
            player.character.playerTrap = player.character.playerTrapTemp;
        }
        ShowTraps();
        abilityText.text = player.character.ability.ToString();
        
        //StartCoroutine(TurnTimer());
    }
    public void EndTurn()
    {
        if(player.inmobilized)
        {
            player.inmobilized = false;
            player.character.murdered = true;
        }
        if(player.character.IsDead()) player.character.health += 1f;
        turnDuration = player.character.turnDuration;
        menuPanel.SetActive(false);
        deathPanel.SetActive(false);
        abilityButton.gameObject.SetActive(false);
        abilityCooldownBar.gameObject.SetActive(true);
        Global.isPaused = false;
        Global.onEndTurn = false;
        if(player.character.ability == Abilities.enhancedMemory)
            DontShowPosition();
        if (Global.players.Count == 0)
        {
            Debug.LogError("No hay jugadores en la lista");
            return;
        }
        Debug.Log($"Terminando turno Jugador actual: {currentPlayer}, Total de jugadores: {Global.players.Count}");
        Global.players[currentPlayer][Global.index].SetActive(false);
        if(container0 != null) 
            container0.RevertTraps(player.character.playerTrap);
        currentPlayer = (currentPlayer + 1) % Global.players.Count;
        //MovPlayer1 player0 = Global.players[currentPlayer][0].GetComponent<MovPlayer1>();
        //player0.total = 0;

        Debug.Log($"Nuevo jugador activo: {currentPlayer}");
        Global.players[currentPlayer][Global.index].SetActive(true);
        StartTurn();
        playerInfo = new int[] {Global.currentPlayer, Global.index};
        playerInfo1 = new int[] {Global.currentPlayer, (Global.index + 1)%2};
        if(Global.atTheGoal.Count > 0)
        Debug.Log($"{Global.atTheGoal[0][0]}, {Global.atTheGoal[0][1]}");
        Debug.Log($"{playerInfo[0]}, {playerInfo[1]}");
        contains = false;
        foreach(var arr in Global.atTheGoal)
        {
            if(arr.SequenceEqual(playerInfo))
            {
                contains = true;
                index = (Global.index + 1)%2;
            }
            if(arr.SequenceEqual(playerInfo1))
            {
                contains = true;
                index = Global.index;
            }
        }
        if(contains)
        {
            Global.isPaused = true;
            selectCharacterPanel.SetActive(true);
            SelectCharacter(index);
        }
        else
        {
            selectCharacterPanel.SetActive(true);
            Global.isPaused = true;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Global.trapP = trapPanel;
        Global.trapT = trapText;
        Global.final = final;
        Global.finalT = finalT;
        final.SetActive(false);
        trapPanel.SetActive(false);
        selectCharacterPanel.SetActive(false);
        if(trapPanel != null) Debug.Log("trapPanel no se ha perdido");
        else Debug.Log("se ha perdio");
        menuPanel.SetActive(false);
        deathPanel.SetActive(false);
        abilityActiveDurationBar.gameObject.SetActive(false);
        abilityButton.gameObject.SetActive(false);
        exitPanel.SetActive(false);
        //trapPanel.SetActive(false);
        InitPlayers();
        container0 = container.GetComponent<Container1>();
        //container1 = container0;
        container0.playersC = new List<GameObject[]>();
        container0.Init(size,Global.players);
        Global.players = container0.playersC;
        map0 = map.GetComponent<MazeMap>();
        map0.UpdateUI(grid);
        Debug.Log($"el count es {Global.players.Count}");
        selectCharacterPanel.SetActive(true);
        //while(selectCharacterPanel.activeSelf){}
        ActivePlayer();
        selectCharacterPanel.SetActive(true);
        Global.isPaused = true;
        StartTurn();
        DesactiveButtons();
        atackP.SetActive(false);
        //isInit = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.character.murdered)
        {
            player.character.health = 0;
            player.character.murdered = false;
        }
        if(Global.onEndTurn && !trapPanel.activeSelf && !deathPanel.activeSelf && !exitPanel.activeSelf && !final.activeSelf && !selectCharacterPanel.activeSelf && !atackP.activeSelf)
        {
            Global.isPaused = true;
            menuPanel.SetActive(true);
        }
        if(player.character.IsDead() && !trapPanel.activeSelf && !selectCharacterPanel.activeSelf) 
        {
            deathPanel.SetActive(true);
            Global.isPaused = true;
        }
        if(turnDuration > 0 && !Global.isPaused)
        {
            turnDuration -= Time.deltaTime;
            timerText.text = $"Tiempo restante: {Mathf.Ceil(turnDuration).ToString()}";
        }
        else
        {
            if(!deathPanel.activeSelf && !trapPanel.activeSelf && !selectCharacterPanel.activeSelf && !final.activeSelf && !atackP.activeSelf)
            {
                Global.isPaused = true;
                Global.onEndTurn = true;
                //menuPanel.SetActive(true);
            }
        }
        if(count > 0 && !Global.isPaused)
            count -= Time.deltaTime;
        if(player.character.poisoned && !Global.isPaused)
        {
            Debug.Log($"envenenao {poisonedTime} {player.character.poisoned}" );
            if(poisonedTime < 0)
            {
                poisonedTime = 10;
                player.character.poisoned = false;
            }
        }
            
        
        if(trapPanel.activeSelf)
            ShowTraps();
        
        UpdateHealthBars();
        UpdateCooldowns();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            exitPanel.SetActive(true);
            Global.isPaused = true;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            UseAbility();
        }
        healthText.text = Mathf.Ceil(player.character.health).ToString();
        if(abilityActiveDurationBar.gameObject.activeSelf)
        {
            abilityTime.text = Math.Floor(player.character.currentActiveTime).ToString();
        }
        else
        {
            abilityTime.text = Mathf.Floor(player.character.currentCooldown).ToString();
        }
        key.text =$"   {Global.countKey0}    {Global.countKey1}    {Global.countKey2}";
        if(Global.atack == true)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                Global.isPaused = false;
                atackP.SetActive(false);
                switch (player.character.ability)
                {
                    case Abilities.poison:
                        Global.players[Global.currentPlayerPoisoned][0].GetComponent<MovPlayer1>().character.poisoned = true;
                        break;
                    case Abilities.atack:
                        Global.players[Global.currentPlayerAtacked][0].GetComponent<MovPlayer1>().character.TakeDamage(15);
                        break;
                    case Abilities.curse:
                        Global.players[Global.currentPlayerCurse][0].GetComponent<MovPlayer1>().character.cursed = true;
                        break;
                    case Abilities.killer:
                        Global.players[Global.currentPlayerMurdered][0].GetComponent<MovPlayer1>().character.murdered = true;
                        break;
                    case Abilities.inmobilize:
                        Global.players[Global.currentPlayerInmobilized][0].GetComponent<MovPlayer1>().inmobilized = true;
                        break;
                    default:
                    break;
                }
                atackText.text = "Para usar tu habilidad elija sobre que jugador quiere que se aplique el efecto";
            }
            if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                Global.isPaused = false;
                atackP.SetActive(false);
                switch (player.character.ability)
                {
                    case Abilities.poison:
                        Global.players[Global.currentPlayerPoisoned][1].GetComponent<MovPlayer1>().character.poisoned = true;
                        break;
                    case Abilities.atack:
                        Global.players[Global.currentPlayerAtacked][1].GetComponent<MovPlayer1>().character.TakeDamage(15);
                        break;
                    case Abilities.curse:
                        Global.players[Global.currentPlayerCurse][1].GetComponent<MovPlayer1>().character.cursed = true;
                        break;
                    case Abilities.killer:
                        Global.players[Global.currentPlayerMurdered][1].GetComponent<MovPlayer1>().character.murdered = true;
                        break;
                    case Abilities.inmobilize:
                        Global.players[Global.currentPlayerInmobilized][1].GetComponent<MovPlayer1>().inmobilized = true;
                        break;
                    default:
                    break;
                }
                atackText.text = "Para usar tu habilidad elija sobre que jugador quiere que se aplique el efecto";
            }
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
        MovPlayer1 player1 = Global.players[currentPlayer][Global.index].GetComponent<MovPlayer1>();
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
            Global.players[currentPlayer][Global.index].SetActive(true); 
        }
    }

    void InitPlayers()
    {
        InitPLayer(Player1, 100, 100, 10, 40f, "s1");
        InitPLayer(Player2, 20, 20, 10, 35f, "s2");
        InitPLayer(Player3, 100, 100, 10, 30f, "s3");
        InitPLayer(Player4, 100, 100, 10, 45f, "s4");
        InitPLayer(Player5, 100, 100, 10, 50f, "s5");
        InitPLayer(Player6, 100, 100, 10, 25f, "s6");
        InitPLayer(Player7, 100, 100, 10, 40f, "s7");
        InitPLayer(Player8, 100, 100, 10, 35f, "s8");
        InitPLayer(Player9, 100, 100, 10, 30f, "s9");
        InitPLayer(Player10, 100, 100, 10, 45f, "s10");
    }

    void InitPLayer(GameObject player, float health, float maxHealth, float speed, float turnD, string skill)
    {
        MovPlayer1 player1 = player.GetComponent<MovPlayer1>();
        player1.character = new Character(health, maxHealth, speed, skill, turnD);
        switch (skill)
        {
            case "s1":
                player1.character.SetAbility(Abilities.atack, 5f, 2f);
                break;
            case "s2":
                player1.character.SetAbility(Abilities.trapDetector, 15f, 20f);
                break;
            case "s3":
                player1.character.SetAbility(Abilities.boom, 240f, 1f);
                break;
            case "s4":
                player1.character.SetAbility(Abilities.inmobilize, 5f, 1f);
                break;
            case "s5":
                player1.character.SetAbility(Abilities.heal, 20f, 1f);
                break;
            case "s6":
                player1.character.SetAbility(Abilities.killer, 15f, 2f);
                break;
            case "s7":
                player1.character.SetAbility(Abilities.enhancedMemory, 60f, 20f);
                break;
            case "s8":
                player1.character.SetAbility(Abilities.poison, 10f, 2f);
                break;
            case "s9":
                player1.character.SetAbility(Abilities.teleport, 15f, 3f);
                break;
            case "s10":
                player1.character.SetAbility(Abilities.curse, 5f, 1f);
                break;
        }
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
        if(player.character.poisoned && count < 3)
        {
            player.character.TakeDamage(5*Time.deltaTime);
            poisonedTime -= Time.deltaTime;
        }
        if(count < 1)
        {
            player.character.Heal(0.5f*Time.deltaTime);
            count = 5;
        }
        healthBar.maxValue = player.character.maxHealth;
        healthBar.minValue = 0f;
        healthBar.value = player.character.health;
        
    }
    void UpdateCooldowns()
    {
        player.character.UpdateCooldowns(Time.deltaTime);
        abilityCooldownBar.fillRect.GetComponent<Image>().color = player.gameObject.GetComponent<Renderer>().material.color;
        abilityCooldownBar.maxValue = player.character.abilityCooldown;
        abilityCooldownBar.minValue = 0f;
        abilityCooldownBar.value = player.character.currentCooldown;

        abilityActiveDurationBar.fillRect.GetComponent<Image>().color = player.gameObject.GetComponent<Renderer>().material.color;
        abilityActiveDurationBar.maxValue = player.character.abilityActiveDuration;
        abilityActiveDurationBar.minValue = 0f;
        abilityActiveDurationBar.value = player.character.currentActiveTime;
        if(abilityCooldownBar.value >= abilityCooldownBar.maxValue)
        {
            abilityCooldownBar.gameObject.SetActive(false);
            abilityButton.gameObject.SetActive(true);
        }
        if(player.character.currentActiveTime <= 0)
        {
            abilityActiveDurationBar.gameObject.SetActive(false);
            abilityCooldownBar.gameObject.SetActive(true);
        }
        else 
            abilityCooldownBar.gameObject.SetActive(false);
    }
    public void UseAbility()
    {
        if(abilityCooldownBar.gameObject.activeSelf)
        {
            abilityCooldownBar.gameObject.SetActive(false);
            abilityActiveDurationBar.gameObject.SetActive(true);
            player.character.UseAbility();
            player.character.currentCooldown = 0f;
            abilityButton.gameObject.SetActive(false);
            if(player.character.cursed)
            {
                player.character.TakeDamage(5);
                player.character.cursed = false;
                return;
            }
            switch(player.character.ability)
            {
                case Abilities.trapDetector:
                    ShowTraps();
                    break;
                case Abilities.boom:
                    Boom();
                    break;
                case Abilities.enhancedMemory:
                    ShowInitialPosition();
                    break;
                default:
                    Atack();
                    break;
            }
        }
    }
    void ShowTraps()
    {
        if(player.character.playerTrap.Count != 0)
        {
            if(container0 != null) container0.ShowTamps(player.character.playerTrap);
        }
    }
    void Boom()
    {
        Desactivate(SearchCollidersAt(player.transform.position.x, player.transform.position.z + 1), "Wall");
        Desactivate(SearchCollidersAt(player.transform.position.x, player.transform.position.z - 1), "Wall");
        Desactivate(SearchCollidersAt(player.transform.position.x - 1, player.transform.position.z), "Wall");
        Desactivate(SearchCollidersAt(player.transform.position.x + 1, player.transform.position.z), "Wall");
    }
    Collider[] SearchCollidersAt(float x, float z)
    {
        Vector3 position = new Vector3(x, 0.25f, z);
        Collider[] colliders = Physics.OverlapSphere(position, 0.1f);
        return colliders;
    }
    void Desactivate(Collider[] colliders, string tag)
    {
        if(colliders != null)
        {
            foreach(var collider in colliders)
            {
                if(collider.gameObject.CompareTag(tag))
                collider.gameObject.SetActive(false);
            }
        }
    }
    void ShowInitialPosition()
    {
        map0.Change((player.character.initialX-1)*size + player.character.initialZ, Color.red);
        Debug.Log($"{player.character.initialX}, {player.character.initialZ}");
    }
    void DontShowPosition()
    {
        map0.Change((player.character.initialX-1)*size + player.character.initialZ, new Color(1f, 1f, 1f, 0.5f));
        Debug.Log($"{player.character.initialX}, {player.character.initialZ}");
    }
    void DesactivateGates()
    {
        Desactivate(SearchCollidersAt(player.transform.position.x, player.transform.position.z + 1), "Gate");
        Desactivate(SearchCollidersAt(player.transform.position.x, player.transform.position.z - 1), "Gate");
        Desactivate(SearchCollidersAt(player.transform.position.x - 1, player.transform.position.z), "Gate");
        Desactivate(SearchCollidersAt(player.transform.position.x + 1, player.transform.position.z), "Gate");
    }
    public void Character1()
    {
        SelectCharacter(0);
    }
    public void Character2()
    {
        SelectCharacter(1);
    }
    public void ExitToMenu()
    {
        Global.players = null;
        Global.isPaused = false;
        SceneManager.LoadScene("Menu");
    }
    public void Continue()
    {
        Global.isPaused = false;
        exitPanel. SetActive(false);
    }
    void SelectCharacter(int index)
    {
        Global.players[currentPlayer][Global.index].SetActive(false);
        Global.index = index;
        Global.players[currentPlayer][Global.index].SetActive(true);
        selectCharacterPanel.SetActive(false);
        Global.isPaused = false;
        StartTurn();
    }
    void Atack()
    {
        atackP.SetActive(true);
        Global.isPaused = true;
        int j = 0;
        for(int i = 0; i < Global.players.Count - 1; i++)
        {
            buttons[i].gameObject.SetActive(true);
            if(i == currentPlayer)
                j ++;
            buttons[i].GetComponent<Image>().color = Global.players[j][0].GetComponent<Renderer>().material.color;
            switch (j)
            {
                case 0:
                    buttons[i].onClick.AddListener(AtackPlayer0);
                break;
                case 1:
                    buttons[i].onClick.AddListener(AtackPlayer1);
                break;
                case 2:
                    buttons[i].onClick.AddListener(AtackPlayer2);
                break;
                case 3:
                    buttons[i].onClick.AddListener(AtackPlayer3);
                break;
                case 4:
                    buttons[i].onClick.AddListener(AtackPlayer4);
                break;
            }
            
            j++;
        }
    }
    void DesactiveButtons()
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }
    }
    void AtackPlayer0()
    {
        AtackPlayer(0);
    }
    void AtackPlayer1()
    {
        AtackPlayer(1);
    }
    void AtackPlayer2()
    {
        AtackPlayer(2);
    }
    void AtackPlayer3()
    {
        AtackPlayer(3);
    }
    void AtackPlayer4()
    {
        AtackPlayer(4);
    }
    void AtackPlayer(int playerIndex)
    {
        DesactiveButtons();
        Global.atack = true;
        atackText.text = "Presione 1 para usar su habilidad sobre el personaje pasivo y 2 para usar su habilidad sobre el personaje activo";
        switch (player.character.ability)
        {
            case Abilities.poison:
                Global.currentPlayerPoisoned = playerIndex;
                break;
            case Abilities.atack:
                Global.currentPlayerAtacked = playerIndex;
                break;
            case Abilities.curse:
                Global.currentPlayerCurse = playerIndex;
                break;
            case Abilities.killer:
                Global.currentPlayerMurdered = playerIndex;
                break;
            case Abilities.inmobilize:
                Global.currentPlayerInmobilized = playerIndex;
                break;
            default:
            break;
        }
    }
    
}
