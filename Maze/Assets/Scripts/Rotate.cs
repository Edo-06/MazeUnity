using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float speed = 0;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate( 0 , speed*Time.deltaTime, 0 );
    }
}
