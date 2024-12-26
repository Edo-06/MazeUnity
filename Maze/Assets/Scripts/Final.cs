using UnityEngine;

public class Final : MonoBehaviour
{


    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Has llegado al objetivo");
        }
    }

}
