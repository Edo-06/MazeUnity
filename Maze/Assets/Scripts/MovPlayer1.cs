using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System;
using UnityEngine.UIElements;
using System.ComponentModel;
using UnityEngine.Rendering;

public class MovPlayer1 : MonoBehaviour
{
    public List<string> keys; 
    
    public Rigidbody rb;
    public Camera playerCamera;
    public GameManager gameManager;
    public float speed = 5f;
    public float rotationSpeed = 128;
    public int i = 0;
    public Character character;
    public GameObject container;
    public Vector3 lastPosition;
    public float total = 0f;
    public Slider healthBar;
    //private bool isInit = false;
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
        //lastPosition = transform.position;

        //isInit=true;
        
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        //rigidbody.MovePosition(new Vector3(2,0.5f,2));
    }

    // Update is called once per frame
    void Update()
    {
        if(Global.isPaused) return;
        else{
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        transform.Translate(new Vector3(horizontal, 0, vertical)*speed*Time.deltaTime);
        float rotationy = Input.GetAxis("Mouse X");
        float rotationx = Input.GetAxis("Mouse Y");
        transform.Rotate(new Vector3(0, rotationy,0)*Time.deltaTime*rotationSpeed);
        //if(Global.players.Count > 1) Next();
        //float distance = Vector3.Distance(lastPosition,transform.position);
        /*total += distance;
        lastPosition = transform.position;
        Debug.Log(total);
        if(total > 20)
        {
            total = 0;
            gameManager.EndTurn();
        }*/
        }
    }


    
    void Init()
    {
        Container1 container0 = gameManager.container.GetComponent<Container1>();
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

