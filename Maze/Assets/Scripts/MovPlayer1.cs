using UnityEngine;
using UnityEngine.AI;

public class Gamer1 : MonoBehaviour
{
    Rigidbody rigidbody;
    public float speed = 10;
    public float rotationSpeed = 64;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        transform.position = new Vector3(-1, 0.5f, -1);
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        transform.Translate(new Vector3(horizontal, 0, vertical)*speed*Time.deltaTime);
        float rotation = Input.GetAxis("Mouse X");
        transform.Rotate(new Vector3(0, rotation,0)*Time.deltaTime*rotationSpeed);
    }
}
