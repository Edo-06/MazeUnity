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

    void OnTurnEnd()
    {
        if (gameManager != null) 
        {
            gameManager.EndTurn();
        }
    }
}