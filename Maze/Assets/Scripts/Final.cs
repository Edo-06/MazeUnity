using UnityEngine;

public class Final : MonoBehaviour
{


    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("entraAAAAAAAAAAAAAAAAAAAAAA");
        if(other.CompareTag("Player"))
        {
            Debug.Log("Has llegado al objetivo");
        }
    }

}
