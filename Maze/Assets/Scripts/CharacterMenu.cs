using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class CharacterMenu : MonoBehaviour
{
    public List<GameObject[]> selectedCharacters = new List<GameObject[]>();
    public GameObject Player1, Player2, Player3, Player4, Player5, Player6, Player7, Player8, Player9, Player10;

    public void SelectCharacter(GameObject P1, GameObject P2)
    {
        GameObject[] characters = new GameObject[] { P1, P2 };
        selectedCharacters.Add(characters);
    }

    public void SelectCouple1Button()
    {
        SelectCharacter(Player2, Player1);
    }

    public void SelectCouple2Button()
    {
        SelectCharacter(Player3, Player4);
    }

    public void SelectCouple3Button()
    {
        SelectCharacter(Player5, Player6);
    }

    public void SelectCouple4Button()
    {
        SelectCharacter(Player7, Player8);
    }
    public void SelectCouple5Button()
    {
        SelectCharacter(Player9, Player10);
    }

    public void BackButton()
    {
        Global.players = selectedCharacters;        
    }
}
