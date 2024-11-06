using UnityEngine;

public class Final : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Debug.Log("entra............");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("entraAAAAAAAAAAAAAAAAAAAAAA");
        if(other.CompareTag("Player"))
        {
            Debug.Log("Has llegado al objetivo");
            other.transform.position = new Vector3(-3, 0.5f, -3);
        }
    }
}
