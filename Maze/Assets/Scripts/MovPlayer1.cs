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
    public Camera playerCamera;
    public GameManager gameManager;
    private float speed = 2f;
    private float rotationSpeed = 90f;
    public Character character;
    public GameObject container;
    public Vector3 lastPosition;
    public bool inmobilized = false;
    public MovPlayer1 Instance;

    public void ActivateCamera()
    {
        playerCamera.gameObject.SetActive(true);
    }

    public void DeactivateCamera()
    {
        playerCamera.gameObject.SetActive(false);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        keys = new List<string>();
        if(Instance == null)
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Global.isPaused || inmobilized) 
            return;
        else
        {
            float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");
            transform.Translate(new Vector3(horizontal, 0, vertical)*speed*Time.deltaTime);
            float rotationy = Input.GetAxis("Mouse X");
            float rotationx = Input.GetAxis("Mouse Y");
            transform.Rotate(new Vector3(0, rotationy,0)*Time.deltaTime*rotationSpeed);
        }
    }   
}

