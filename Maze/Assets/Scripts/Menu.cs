using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject initialP, selectCP;
    public Button playB;
    public List<GameObject[]> selectedCharacters = new List<GameObject[]>();
    public GameObject Player1, Player2, Player3, Player4, Player5, Player6, Player7, Player8, Player9, Player10;
    private List<int> check = new List<int>();

    void Start()
    {
        selectCP.SetActive(false);
        playB.gameObject.SetActive(false);
    }

    public void Play()
    {
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
    }

    void SelectCharacter(GameObject[] characters, int index)
    {
        if (!check.Contains(index))
        {
            check.Add(index);
            selectedCharacters.Add(characters);
        }
    }

    public void SelectCouple1Button()
    {
        SelectCharacter(new GameObject[] { Player2, Player1 }, 1);
    }

    public void SelectCouple2Button()
    {
        SelectCharacter(new GameObject[] { Player3, Player4 }, 2);
    }

    public void SelectCouple3Button()
    {
        SelectCharacter(new GameObject[] { Player5, Player6 }, 3);
    }

    public void SelectCouple4Button()
    {
        SelectCharacter(new GameObject[] { Player7, Player8 }, 4);
    }

    public void SelectCouple5Button()
    {
        SelectCharacter(new GameObject[] { Player9, Player10 }, 5);
    }

    public void BackButton()
    {
        Global.players = selectedCharacters;
        selectCP.SetActive(false);
        initialP.SetActive(true);
        if (Global.players.Count != 0) playB.gameObject.SetActive(true);
    }
}
