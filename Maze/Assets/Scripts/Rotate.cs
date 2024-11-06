using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float speed = 8192;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate( 0 , speed*Time.deltaTime, 0 );
    }
}
