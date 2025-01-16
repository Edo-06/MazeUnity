
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Final : MonoBehaviour
{
    public Button btn1, btn2;
    void Start()
    {
        btn1.gameObject.SetActive(false);
        btn2.gameObject.SetActive(false);
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            int[] playerInfo = new int[] {Global.currentPlayer, Global.index};
            int[] playerInfo1 = new int[] {Global.currentPlayer, (Global.index + 1)%2};
            if(Global.atTheGoal.Contains(playerInfo1))
            {
                Global.final.SetActive(true);
                Global.isPaused = true;
                Global.finalT.text = "Han llegado ambos al objetivo final, ha terminado el juego";
                btn1.gameObject.SetActive(false);
                btn2.gameObject.SetActive(true);
            }
            else
            {
                Global.atTheGoal.Add(playerInfo);
                Debug.Log("meta");
                Global.final.SetActive(true);
                Global.isPaused = true;
                Global.finalT.text = "Has llegado al objetivo";
                btn1.gameObject.SetActive(true);
                btn2.gameObject.SetActive(false);
            }
        }
    }
    public void ClickF()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Click()
    {
        Global.final.SetActive(false);
    }
}
