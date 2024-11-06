using UnityEngine;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;



public class Container1 : MonoBehaviour
{
    public GameObject WallObject;
    public GameObject ObstacleObject0;
    public  GameObject ObstacleObject1;
    public  GameObject ObstacleObject2;


    public GameObject LimitObject;
    public GameObject FinalObject;
    public GameObject Player1;
    public GameObject Key0;
    public GameObject Key1;
    public GameObject Key2;


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
                        switch(maze.mazee[i,j].type)
                        {   case "type0":
                                GameObject obstacle0 = Instantiate(ObstacleObject0, new Vector3(i, 0, j), Quaternion.identity);
                                Obstacle obstacle0c = obstacle0.GetComponent<Obstacle>();
                                obstacle0c.type = "type0";
                            if(maze.mazee[i,j].modo == "vertical")
                                obstacle0.transform.localScale = new Vector3(0.2f, 0.5f, 1);
                            else
                                obstacle0.transform.localScale = new Vector3(1, 0.5f, 0.2f);
                            break;
                            case "type1":
                            GameObject obstacle1 = Instantiate(ObstacleObject1, new Vector3(i, 0, j), Quaternion.identity);
                            Obstacle obstacle1c = obstacle1.GetComponent<Obstacle>();
                            obstacle1c.type = "type1";
                            if(maze.mazee[i,j].modo == "vertical")
                                obstacle1.transform.localScale = new Vector3(0.2f, 0.5f, 1);
                            else
                                obstacle1.transform.localScale = new Vector3(1, 0.5f, 0.2f);
                            break;
                            case  "type2":
                            GameObject obstacle2 = Instantiate(ObstacleObject2, new Vector3(i, 0, j), Quaternion.identity);
                            Obstacle obstacle2c = obstacle2.GetComponent<Obstacle>();
                            obstacle2c.type = "type2";
                            if(maze.mazee[i,j].modo == "vertical")
                                obstacle2.transform.localScale = new Vector3(0.2f, 0.5f, 1);
                            else
                                obstacle2.transform.localScale = new Vector3(1, 0.5f, 0.2f);
                            break;
                        }
                        break;

                        
                    case Category.final:
                        GameObject final = Instantiate(FinalObject, new Vector3(i, 0.5f, j), Quaternion.identity);
                    break;
                    case Category.key:
                        switch(maze.mazee[i,j].type)
                        {
                            case "type0":
                                GameObject key0 = Instantiate(Key0, new Vector3(i, 0.25f, j), Quaternion.identity);
                                key0.transform.localScale = new Vector3(0.3f,0.3f,0.3f);
                                Key key0C =  key0.GetComponent<Key>();
                                key0C.type = "type0";
                                break;
                            case  "type1":
                                GameObject key1 = Instantiate(Key1, new Vector3(i, 0.25f, j), Quaternion.identity);
                                key1.transform.localScale = new Vector3(0.3f,0.3f,0.3f);
                                Key key1C =  key1.GetComponent<Key>();
                                key1C.type = "type1";
                                break;
                            case   "type2":
                                GameObject key2 = Instantiate(Key2, new Vector3(i, 0.25f, j), Quaternion.identity);
                                key2.transform.localScale = new Vector3(0.3f,0.3f,0.3f);
                                Key key2C =  key2.GetComponent<Key>();
                                key2C.type = "type2";
                                break;
                        }
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




