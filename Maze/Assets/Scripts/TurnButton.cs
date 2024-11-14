using UnityEngine;
using UnityEngine.UI;

public class TurnButton : MonoBehaviour
{
    public GameManager gameManager;
    public Button btn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Button btn0 = btn.GetComponent<Button>();
        btn0.onClick.AddListener(OnTurnEnd);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTurnEnd()
    {
        //Debug.Log("aqui");
        if (gameManager != null) 
        {
            //Debug.Log("aa");
            gameManager.EndTurn();
        }
        else
        {
            Debug.LogError("GameManager no está asignado en TurnButton.");
        }
    }
}