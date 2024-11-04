using UnityEngine;
using System;
using System.Collections.Generic;



public class Container1 : MonoBehaviour
{
    public GameObject WallObject;
    public Maze maze = new Maze();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
            int size = 30;
            maze.Generator(size);
            Print(size);

            
            //GameObject wall = Instantiate(WallObject, new Vector3(5, 0, 5), Quaternion.identity);
               // wall.transform.localScale = new Vector3(1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Print(int size)
    {
        for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if(maze.mazee[i, j].category  == Category.wall)
                    {
                        GameObject wall = Instantiate(WallObject, new Vector3(i, 1, j), Quaternion.identity);
                    }
                }
            }
    }
}




