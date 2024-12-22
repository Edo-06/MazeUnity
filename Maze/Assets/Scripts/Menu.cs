using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        Debug.Log("Play");
        if (Global.players.Count == 0)
        {
            Debug.LogError("No hay jugadores en la lista");
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
        SceneManager.LoadScene("Characters");
    }
}
