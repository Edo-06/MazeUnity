using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System;
using UnityEngine.UIElements;
using System.ComponentModel;

public class MovPlayer1 : MonoBehaviour
{
    public List<string> keys; 
    public Rigidbody rigidbody;
    public Camera playerCamera;
    public GameManager gameManager;
    public float speed = 5f;
    public float rotationSpeed = 128;
    public int i = 0;
    public Character character;
    private Rigidbody rb;
    public GameObject container;
    public Vector3 lastPosition;
    public float total;
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
        lastPosition = transform.position;
        total = 0f;

        //isInit=true;
        
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
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
        //if(Global.players.Count > 1) Next();
        float distance = Vector3.Distance(lastPosition,transform.position);
        total += distance;
        lastPosition = transform.position;
        Debug.Log(total);
        if(total > 20)
        {
            total = 0;
            gameManager.EndTurn();
        }
    }


    void Next()
    {
        
        /*if (gameManager == null)
        {
            Debug.LogError("gameManager is not assigned.");
            return; // Salir si gameManager es null
        }

        if (container == null)
        {
            Debug.LogError("container is not assigned.");
            return; // Salir si container es null
        }

        if (container0.maze == null)
        {
            Debug.LogError("maze is not assigned.");
            return; // Salir si maze es null
        }*/

        List<int[]> boundary = BoundaryCells(transform.position.x, transform.position.z, 1);
        
        if (boundary == null || boundary.Count == 0)
        {
            Debug.LogWarning("BoundaryCells returned null or an empty list.");
            return; 
        }

        for (int i = 0; i < boundary.Count; i++) 
        {
            if (Math.Abs(transform.position.x - boundary[i][0]) < 0 && Math.Abs(transform.position.z - boundary[i][1]) < 0) // poner en un rango 
            {
                gameManager.EndTurn();
            }
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

    public List<int[]> BoundaryCells(float rowFloat, float colFloat, float distance)
        {
            int row = (int)rowFloat;
            int col = (int)colFloat;


            List<int[]> neighbords = new List<int[]>();
            List<int[]> usedcells0 = new List<int[]>();
            Neighborhod(neighbords, usedcells0, row, col);
            List<int[]> boundary = Path(neighbords, usedcells0, distance);
            return boundary;
        }

    private void Neighborhod(List<int[]> neighbords, List<int[]> usedcells0, int row, int col)
    {   //Container1 container0 = container.GetComponent<Container1>();
        usedcells0.Add(new int[] {row, col});
        if (row >= 1 && Global.maze.mazee[row - 1, col].category == Category.floor &&  !usedcells0.Contains(new int[] { row - 1, col }))
            neighbords.Add(new int[] { row - 1, col });
        if (col >= 1 && Global.maze.mazee[row, col - 1].category == Category.floor  && !usedcells0.Contains(new int[] { row, col - 1 }))
            neighbords.Add(new int[] { row, col - 1 });
        if (row <= gameManager.size - 2 && Global.maze.mazee[row + 1, col].category == Category.floor  && !usedcells0.Contains(new int[] { row + 1, col }))
            neighbords.Add(new int[] { row + 1, col });
        if (col <= gameManager.size - 2 && Global.maze.mazee[row, col + 1].category == Category.floor   && !usedcells0.Contains(new int[] { row, col + 1 }))
            neighbords.Add(new int[] { row, col + 1 });
    }

    private List<int[]> Path(List<int[]> neighbords,List<int[]> usedcells0, float distance)
    {
        int x=0;
        int y=0;
        List<int[]> boundary = new List<int[]>();

        if(distance <= 0)
        {
            x = usedcells0[usedcells0.Count - 1][0];
            y = usedcells0[usedcells0.Count - 1][1];
            boundary.Add(new int[]{x, y});
        }

        while(distance > 0)
        {
            if(distance  > 1)
            {
                for (int i = 0; i < neighbords.Count; i++)
                {
                    Neighborhod(neighbords, usedcells0, neighbords[neighbords.Count-i-1][0], neighbords[neighbords.Count-i-1][1]);
                    neighbords.RemoveAt(i);
                    if (distance > 0)
                    {
                        distance--;
                        var result = Path(neighbords, usedcells0, distance); 
                        boundary.AddRange(result); 
                    }
                }
            }    
            else
            {
                for(int i = 0; i < neighbords.Count; i++)
                {
                    boundary.Add(neighbords[i]);
                    
                }
                distance--;
            }
        }    
        
        return boundary; 
    }
}
