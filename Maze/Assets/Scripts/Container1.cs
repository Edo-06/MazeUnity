using UnityEngine;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;
using System;
using UnityEngine.InputSystem.Switch;



public class Container1 : MonoBehaviour
{
    public List<GameObject[]> playersC;
    public GameObject WallObject;
    public GameObject ObstacleObject0;
    public  GameObject ObstacleObject1;
    public  GameObject ObstacleObject2;


    public GameObject LimitObject;
    public GameObject FinalObject;
    public GameObject Trap;
    public GameObject Key0;
    public GameObject Key1;
    public GameObject Key2;
    public int finalX, finalZ;
    public int size;


    

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
                switch (Global.maze.mazee[i, j].category)
                {
                    case Category.wall:
                        GameObject wall = Instantiate(WallObject, new Vector3(i, 0.5f, j), Quaternion.identity);
                        switch (Global.maze.mazee[i, j].modo)
                        {
                            case "horizontal":
                                wall.transform.localScale = new Vector3(1, 1, 0.2f);
                                break;
                            case "vertical":
                                wall.transform.localScale = new Vector3(0.2f, 1, 1);
                                break;
                            case "corner":
                                wall.transform.localScale = new Vector3(1, 1, 1);
                                break;
                            default:
                                wall.transform.localScale = new Vector3(1, 1, 1);
                                break;
                        }
                        break;
                    case Category.floor:
                        break;
                    case Category.obstacle:
                        switch(Global.maze.mazee[i,j].type)
                        {   case "type0":
                                GameObject obstacle0 = Instantiate(ObstacleObject0, new Vector3(i, 0, j), Quaternion.identity);
                                Obstacle obstacle0c = obstacle0.GetComponent<Obstacle>();
                                obstacle0c.type = "type0";
                            if(Global.maze.mazee[i,j].modo == "vertical")
                                obstacle0.transform.localScale = new Vector3(0.2f, 0.5f, 1);
                            else
                                obstacle0.transform.localScale = new Vector3(1, 0.5f, 0.2f);
                            break;
                            case "type1":
                            GameObject obstacle1 = Instantiate(ObstacleObject1, new Vector3(i, 0, j), Quaternion.identity);
                            Obstacle obstacle1c = obstacle1.GetComponent<Obstacle>();
                            obstacle1c.type = "type1";
                            if(Global.maze.mazee[i,j].modo == "vertical")
                                obstacle1.transform.localScale = new Vector3(0.2f, 0.5f, 1);
                            else
                                obstacle1.transform.localScale = new Vector3(1, 0.5f, 0.2f);
                            break;
                            case  "type2":
                            GameObject obstacle2 = Instantiate(ObstacleObject2, new Vector3(i, 0, j), Quaternion.identity);
                            Obstacle obstacle2c = obstacle2.GetComponent<Obstacle>();
                            obstacle2c.type = "type2";
                            if(Global.maze.mazee[i,j].modo == "vertical")
                                obstacle2.transform.localScale = new Vector3(0.2f, 0.5f, 1);
                            else
                                obstacle2.transform.localScale = new Vector3(1, 0.5f, 0.2f);
                            break;
                        }
                        break;

                        
                    case Category.final:
                        GameObject final = Instantiate(FinalObject, new Vector3(i, 0.5f, j), Quaternion.identity);
                        finalX = i;
                        finalZ = j;
                    break;
                    case Category.key:
                        switch(Global.maze.mazee[i,j].type)
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
                    case Category.trap:
                        GameObject trap = Instantiate(Trap, new Vector3(i, 0.25f, j), Quaternion.identity);
                        if(Global.maze.mazee[i, j].modo == "vertical")
                        trap.transform.localScale = new Vector3(0.2f, 1, 1);
                        else
                        trap.transform.localScale = new Vector3(1, 1, 0.2f);
                        //trap.transform.parent = Trap.transform;
                        Global.allTheTraps.Add(new int[]{i,j});
                        break;
                }
            }
        }
    }

    void Limit(int size)//no pincha
    {
        for (int i = -2; i < size+1; i++)
        {
                Instantiate(LimitObject, new Vector3(i, 0.5f, -2), Quaternion.identity);
                Instantiate(LimitObject, new Vector3(-2, 0.5f, i), Quaternion.identity);
                Instantiate(LimitObject, new Vector3(size+1, 0.5f, i), Quaternion.identity);
                Instantiate(LimitObject, new Vector3(i, 0.5f,size+1),Quaternion.identity);
        }
    }



    void InitialPosition(List<GameObject[]> players0)
        {
            //Rigidbody Player1Rigidbody;
            System.Random random = new System.Random();
            int x,z,x1,z1, minX, maxX, minZ, maxZ;
            int i = 0;
            int[] arr = {finalX*finalZ, finalX*(size - finalZ), (size-finalX)*finalZ, (size-finalX)*(size-finalZ)};
            int index = IndexOfMaxArr(arr);
            switch(index)
            {
                case 0:
                    minX = 0;
                    minZ = 0;
                    maxX = finalX*2/3;
                    maxZ = finalZ*2/3;
                    break;
                case 1:
                    minX = 0;
                    maxX = finalX*2/3;
                    minZ = finalZ + (size - finalZ)/3;
                    maxZ = size - 1;
                    break;
                case 2:
                    minX = finalX + (size - finalX)/3;
                    maxX = size - 1;
                    minZ = 0;
                    maxZ = finalZ*2/3;
                    break;
                case 3:
                    minX = finalX + (size - finalX)/3;
                    maxX = size - 1;
                    minZ = finalZ*2/3;
                    maxZ = size - 1;
                    break;
                default:
                    minX = 0;
                    maxX = size - 1;
                    minZ = 0;
                    maxZ = size - 1;
                    break;
            }
            while(i < 1)
            {
                x = random.Next(minX,maxX);
                z = random.Next(minZ,maxZ);
                x1 = random.Next(minX,maxX);
                z1 = random.Next(minZ,maxZ);
                if(Global.maze.mazee[x,z].category == Category.floor && Global.maze.mazee[x1,z1].category == Category.floor)
                {
                    foreach(var player in players0)
                    {
                        GameObject[] newPlayer = new GameObject[2];
                        newPlayer[0] = A(player[0],x,z);
                        newPlayer[1] = A(player[1],x1,z1);
                        playersC.Add(newPlayer);
                    }
                    //Debug.Log(player0.character.skill);
                    i++;
                    //Debug.Log(p1.transform.position);
                }
            }
            
        }

        public void Init(int size, List<GameObject[]> players)
        {
            this.size = size;
            Global.maze = new Maze(size);
            Global.maze.Generator(size);
            Limit(size);
            Print(size);
                InitialPosition(players);    
        }
        public void ShowTamps(List<int[]> traps)
        {
            foreach(var trap in traps)
            {
                ChangeColorAtPosition(trap[0], trap[1], Color.red, 0.7f);
                //Instantiate(trampsSeen, new Vector3(trap[0], 0.25f, trap[1]), Quaternion.identity);
            }
        }
        public void RevertTraps(List<int[]> traps)
        {
            foreach(var trap in traps)
            {
                ChangeColorAtPosition(trap[0], trap[1], Color.white, 0f);
            }
        }
        void ChangeColorAtPosition(int x, int z, Color color, float alpha)
        {
            Vector3 position = new Vector3(x, 0.25f , z);
            Collider[] colliders = Physics.OverlapSphere(position, 0.1f);
            foreach (Collider collider in colliders)
            {
                //collider.gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                Renderer rend = collider.GetComponent<Renderer>();
                if (rend != null)
                {
                    rend.material.shader = Shader.Find("Standard");
                    Color newColor = color;
                    newColor.a = alpha; 
                    rend.material.color = newColor;
                    
                    // transparencia 
                    rend.material.SetFloat("_Mode", 3);
                    rend.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    rend.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    rend.material.SetInt("_ZWrite", 0);
                    rend.material.DisableKeyword("_ALPHATEST_ON");
                    rend.material.EnableKeyword("_ALPHABLEND_ON");
                    rend.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    rend.material.renderQueue = 3000;
                }
            }
        }
        GameObject A(GameObject player1,int x, int z)
        {
            MovPlayer1 originalPlayerComponent1 = player1.GetComponent<MovPlayer1>();
                    GameObject p1 = Instantiate(player1, new Vector3(x, 0, z), Quaternion.identity);
                    p1.transform.localScale = new Vector3(0.05f, 0.05f, 0.08f);
                    MovPlayer1 p01 = p1.GetComponent<MovPlayer1>();
                    p01.character = originalPlayerComponent1.character;
                    p01.character.initialX = x;
                    p01.character.initialZ = z;
                    return p1;
        }
        int IndexOfMaxArr(int[] arr)
        {
            int max = arr[0];
            int index = 0;
            for(int i = 1; i < arr.Length; i++)
            {
                if(arr[i] > max)
                {
                    max = arr[i];
                    index = i;
                }
            }
            return index;
        }
}




