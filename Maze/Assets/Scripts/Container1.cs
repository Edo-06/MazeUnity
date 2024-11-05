using UnityEngine;
using System;
using System.Collections.Generic;



public class Container1 : MonoBehaviour
{
    public GameObject WallObject;
    public GameObject ObstacleObject;
    public GameObject LimitObject;
    public Maze maze = new Maze();

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
            int size = 100;
            maze.Generator(size);
            Print(size);
            Limit(size);

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
                switch (maze.mazee[i,j].category)
                {
                    case Category.wall:
                        GameObject wall = Instantiate(WallObject, new Vector3(i, 0.5f, j), Quaternion.identity);
                        wall.transform.localScale = new Vector3(1, 1, 1); // Ajusta el tamaño si es necesario
                        break;
                    case Category.floor:
                        break;
                    case Category.obstacle:
                        GameObject obstacle = Instantiate(ObstacleObject, new Vector3(i, 0, j), Quaternion.identity);
                        obstacle.transform.localScale = new Vector3(0.5f, 1, 0.5f);
                        break;
                    case Category.key:
                        //Console.Write("🗝️");
                        break;
                    case Category.tramp:
                        //Console.Write("💀");
                        break;
                }
                }
            }
    }

    void Limit(int size)
    {
        for (int i = -2; i < size+1; i++)
        {
                GameObject wall = Instantiate(LimitObject, new Vector3(i, 0.5f, -2), Quaternion.identity);
                GameObject wall1 = Instantiate(LimitObject, new Vector3(-2, 0.5f, i), Quaternion.identity);
                GameObject wall2 = Instantiate(LimitObject, new Vector3(size+1, 0.5f, i), Quaternion.identity);
                GameObject wall3 = Instantiate(LimitObject, new Vector3(i, 0.5f,size+1),Quaternion.identity);
        }

    }
}




