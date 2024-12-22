using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class CharacterMenu : MonoBehaviour
{
    public List<GameObject[]> selectedCharacters = new List<GameObject[]>();
    public GameObject Player1, Player2, Player3, Player4, Player5, Player6, Player7, Player8, Player9, Player10;

    public void SelectCharacter(GameObject[] characters)
    {
            selectedCharacters.Add(characters);
    }
    public void DeselectCharacter(GameObject[] characters)
    {
        if (selectedCharacters.Contains(characters))
        {
            selectedCharacters.Remove(characters);
        }
    }
    public void SelectCouple1Button()
    {
        GameObject[] characters = new GameObject[] { Player2, Player1 };
        SelectCharacter(characters);
    }
    public void SelectCouple2Button()
    {
        GameObject[] characters = new GameObject[] { Player3, Player4 };
        SelectCharacter(characters);
    }
    public void SelectCouple3Button()
    {
        GameObject[] characters = new GameObject[] { Player5, Player6 };
        SelectCharacter(characters);
    }
    public void SelectCouple4Button()
    {
        GameObject[] characters = new GameObject[] { Player7, Player8 };
        SelectCharacter(characters);
    }
    public void SelectCouple5Button()
    {
        GameObject[] characters = new GameObject[] { Player9, Player10 };
        SelectCharacter(characters);
    }
    public void BackButton()
    {
        Global.players = selectedCharacters;
        SceneManager.LoadScene("Menu");
    }
}
