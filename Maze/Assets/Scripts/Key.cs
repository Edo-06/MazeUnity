using UnityEngine;

public class Key : MonoBehaviour
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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Key collected" +  type);
            gameObject.SetActive(false);
            MovPlayer1 other0 =  other.gameObject.GetComponent<MovPlayer1>();
            AddObstacleType(type,other0);
            for(int i = 0; i < other0.keys.Count; i++)
            {Debug.Log(other0.keys[i]);}
        }
    }

    private void AddObstacleType(string type, MovPlayer1 other0)
        {
            int i = 0 ;
            bool found = false;
            if(other0.keys.Count == 0)
            {
                other0.keys.Add(type);
            } else 
            {
                while( !found && i < other0.keys.Count)
                {
                    if(type != other0.keys[i])
                        i++;
                    else
                        found = true;
                }
                if(!found)
                    other0.keys.Add(type);
            }
        }
}
