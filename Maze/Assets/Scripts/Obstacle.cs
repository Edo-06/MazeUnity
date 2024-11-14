using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public string type;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit the obstacle");
            MovPlayer1 other0 = other.gameObject.GetComponent<MovPlayer1>();
            if (other0.keys.Contains(type))
            {
                Debug.Log("Tenias la llave, has pasado");
                transform.localScale=new Vector3(0,0,0);
                other0.keys.Remove(type);
                
            }
            else 
            {
                Debug.Log("Busca una llave de " + type);
            }
            Debug.Log(other0.character.skill);
        }
    }

}
