using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class MovPlayer1 : MonoBehaviour
{
    public List<string> keys; 
    public Rigidbody rigidbody;
    public Camera playerCamera;
    public float speed = 10;
    public float rotationSpeed = 128;
    public int i = 0;
    public void TakeTurn()
    {
        Debug.Log("");
    }
    public void ActivateCamera()
    {
        playerCamera.gameObject.SetActive(true);
    }
    public void DeactivateCamera()
    {
        playerCamera.gameObject.SetActive(false);
    }
    /*public void DesactivatePlayer()
    {
        SetActive(false);
    }*/

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        keys = new List<string>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //rigidbody.MovePosition(new Vector3(2,0.5f,2));
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        transform.Translate(new Vector3(horizontal, 0, vertical)*speed*Time.deltaTime);
        float rotationy = Input.GetAxis("Mouse X");
        float rotationx = Input.GetAxis("Mouse Y");
        transform.Rotate(new Vector3(0, rotationy,0)*Time.deltaTime*rotationSpeed);

    }

    
    
   /* void OnTriggerEnter(Collider other)
    {
        Debug.Log("entro L TRIGERRRRRRRRRRRRR");
        if(other.CompareTag("FinalObject"))
        {
            Debug.Log("Has llegado al objetivo");
        }
    }*/

    
}
