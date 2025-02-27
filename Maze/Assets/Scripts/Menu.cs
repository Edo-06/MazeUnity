using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
using Unity.VisualScripting;

public class Menu : MonoBehaviour
{
    public GameObject initialP, selectCP, abilitiesP;
    public Button playB;
    public Button[] buttons;
    public List<GameObject[]> selectedCharacters = new List<GameObject[]>();
    public GameObject Player1, Player2, Player3, Player4, Player5, Player6, Player7, Player8, Player9, Player10;
    private List<int> check = new List<int>();

    void Start()
    {
        selectCP.SetActive(false);
        playB.gameObject.SetActive(false);
        abilitiesP.SetActive(false);

    }

    public void Play()
    {
        MusicManager.Instance.AddSound(MusicManager.Instance.buttonSound);
        if (Global.players == null)
        {
            SelectPlayer();
            MusicManager.Instance.Change(MusicManager.Instance.audioMenu);
        }
        else
        {
            MusicManager.Instance.Change(MusicManager.Instance.audioPlay);
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
    public void Abilities()
    {
        initialP.SetActive(false);
        abilitiesP .SetActive(true);
    }
    public void Back()
    {
        MusicManager.Instance.AddSound(MusicManager.Instance.buttonSound);
        initialP.SetActive(true);
        abilitiesP.SetActive(false);
    }

    void SelectCharacter(GameObject[] characters, int index)
    {
        if (!check.Contains(index))
        {
            check.Add(index);
            selectedCharacters.Add(characters);
        }
        Global.players = selectedCharacters;
        if(Global.players.Count > 1)
            playB.gameObject.SetActive(true);
    }

    public void SelectCouple1Button()
    {
        SelectCharacter(new GameObject[] { Player2, Player1 }, 1);
        buttons[0].interactable = false;
    }

    public void SelectCouple2Button()
    {
        SelectCharacter(new GameObject[] { Player3, Player4 }, 2);
        buttons[1].interactable = false;
    }

    public void SelectCouple3Button()
    {
        SelectCharacter(new GameObject[] { Player5, Player6 }, 3);
        buttons[2].interactable = false;
    }

    public void SelectCouple4Button()
    {
        SelectCharacter(new GameObject[] { Player7, Player8 }, 4);
        buttons[3].interactable = false;
    }

    public void SelectCouple5Button()
    {
        SelectCharacter(new GameObject[] { Player9, Player10 }, 5);
        buttons[4].interactable = false;
    }

    public void BackButton()
    {
        MusicManager.Instance.AddSound(MusicManager.Instance.buttonSound);
        Global.players = selectedCharacters;
        selectCP.SetActive(false);
        initialP.SetActive(true);
        if (Global.players.Count != 0) playB.gameObject.SetActive(true);
    }
}
