using System;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraG1 : MonoBehaviour
{
    public GameObject gamer1;
    Vector3 distance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        distance = transform.position - gamer1.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = gamer1.transform.position + distance;
        //transform.rotation = gamer1.transform.rotation;
        if (Input.GetKey(KeyCode.RightArrow)) 
        {
            transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * 90); 
        }
        if (Input.GetKey(KeyCode.LeftArrow)) 
        {
            transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * 270); 
        }

    }
}
