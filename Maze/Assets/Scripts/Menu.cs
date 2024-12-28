using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject initialP, selectCP;
    public Button playB;//, select1, select2, select3, select4, select5;
    public List<GameObject[]> selectedCharacters = new List<GameObject[]>();
    public GameObject Player1, Player2, Player3, Player4, Player5, Player6, Player7, Player8, Player9, Player10;
    private List<int> check = new List<int>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        selectCP.SetActive(false);
        playB.gameObject.SetActive(false);
       // fondo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        Debug.Log("Play");
        if (Global.players == null)
        {
            Debug.Log("No hay jugadores en la lista");
        }
        else
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void SelectPlayer()
    {
        initialP.SetActive(false);
        selectCP.SetActive(true);
        //fondo.SetActive(true);
    }

    void SelectCharacter(GameObject[] characters)
    {
        selectedCharacters.Add(characters);
    }
    public void SelectCouple1Button()
    {
        if(!check.Contains(1))
        {
            check.Add(1);
            GameObject[] characters = new GameObject[] { Player2, Player1 };
            SelectCharacter(characters);
        }
    }
    public void SelectCouple2Button()
    {
        if(!check.Contains(2))
        {
            check.Add(2);
            GameObject[] characters = new GameObject[] { Player3, Player4 };
            SelectCharacter(characters);
        }
    }
    public void SelectCouple3Button()
    {
        if(!check.Contains(3))
        {
            check.Add(3);
            GameObject[] characters = new GameObject[] { Player5, Player6 };
            SelectCharacter(characters);
        }
    }
    public void SelectCouple4Button()
    {
        if(!check.Contains(4))
        {
            check.Add(4);
            GameObject[] characters = new GameObject[] { Player7, Player8 };
            SelectCharacter(characters);
        }
    }
    public void SelectCouple5Button()
    {
        if(!check.Contains(5))
        {
            check.Add(5);
            GameObject[] characters = new GameObject[] { Player9, Player10 };
            SelectCharacter(characters);
        }
    }
    public void BackButton()
    {
        Global.players = selectedCharacters;
        selectCP.SetActive(false);
        //fondo.SetActive(false);
        initialP.SetActive(true);
        if(Global.players.Count != 0) playB.gameObject.SetActive(true);
    }
}
