using UnityEngine;

public class Key : MonoBehaviour
{
    public string type;
    private float speed = 256f;

    void Update()
    {
        transform.Rotate( 0 , speed*Time.deltaTime, 0 );
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            MovPlayer1 other0 =  other.gameObject.GetComponent<MovPlayer1>();
            SearchType(type,other0);

            switch(type)
            {
                case "type0":
                    other0.character.countKey0 ++;
                    break;
                case "type1":
                    other0.character.countKey1 ++;
                    break;
                case "type2":
                    other0.character.countKey2 ++;
                    break;
            }        
        }
    }

    private void SearchType(string type, MovPlayer1 other0)
    {
        int i = 0 ;
        bool found = false;
        if(other0.keys.Count == 0)
        {
            other0.keys.Add(type);
        } 
        else 
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
