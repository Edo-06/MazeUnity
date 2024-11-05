using UnityEngine;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;



public class Container1 : MonoBehaviour
{
    public GameObject WallObject;
    public GameObject ObstacleObject;
    public GameObject LimitObject;
    public GameObject FinalObject;
    public GameObject Player1;
    public Maze maze = new Maze();
    

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
            maze.Generator(maze.size);
            Limit(maze.size);
            Final(maze.size);
            Print(maze.size);
            InitialPosition();

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
                        if(maze.mazee[i,j].modo == "vertical")
                        obstacle.transform.localScale = new Vector3(0.2f, 0.5f, 1);
                        else
                            obstacle.transform.localScale = new Vector3(1, 0.5f, 0.2f);
                        break;
                    case Category.final:
                        GameObject final = Instantiate(FinalObject, new Vector3(i, 0.1f, j), Quaternion.identity);
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

    void Final(int size)
    {
        int i = 0;
        System.Random random = new System.Random();
        int x, z;
        while(i < 1)
        {
            x = random.Next(0,size);
            z = random.Next(0,size);
            if(maze.mazee[x, z].category == Category.wall 
            || maze.mazee[x, z].category == Category.floor)
            {
                    maze.mazee[x, z].category = Category.final;
                i++;}
        }  
    }

    void InitialPosition()
        {
            //Rigidbody Player1Rigidbody;
            System.Random random = new System.Random();
            int x;
            int z;
            int i = 0;
            while(i < 1)
            {
                x = random.Next(0,maze.size-1);
                z = random.Next(0,maze.size-1);
            if(maze.mazee[x,z].category == Category.floor)
            {
                //Player1Rigidbody = Player1.GetComponent<Rigidbody>();
                //Player1Rigidbody.MovePosition(new Vector3(x, 0.5f, z));
                GameObject player = Instantiate(Player1, new Vector3(x, 0, z), Quaternion.identity);
                i++;
            }
            }
            Debug.Log("aaa");
            Debug.Log(Player1.transform.position);
        }
}




