using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;


public class Tramp : MonoBehaviour
{
    System.Random random = new System.Random();
    //private GameObject trampPanel = Global.trampP;
    private TMP_Text trampText = Global.trapT;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            int rnd = random.Next(0,3);
            //rnd = 0;
            Debug.Log("trampita");
            MovPlayer1 other0 = other.GetComponent<MovPlayer1>();
            int x = (int)Math.Floor(transform.position.x);
            int z = (int)Math.Floor(transform.position.z);
            other0.character.playerTrap.Add(new int[]{x,z});
            string position;
            if(Global.maze.mazee[x,z].modo == "vertical")
            {
                if(other0.transform.position.x > x)
                {
                    other0.transform.position = new Vector3(other0. transform.position.x - 1.5f, other0.transform.position.y, other0.transform.position.z);
                    position = "x+";
                }
                else
                {
                    other0.transform.position = new Vector3(other0. transform.position.x + 1.5f, other0.transform.position.y, other0.transform.position.z);
                    position = "x-";
                }
                
            }
            else
            {
                if(other0.transform.position.z > z)
                {
                    other0.transform.position = new Vector3(other0. transform.position.x, other0.transform.position.y, other0.transform.position.z - 1.5f);
                    position = "z+";
                }
                else
                {
                    other0.transform.position = new Vector3(other0. transform.position.x, other0.transform.position.y, other0.transform.position.z + 1.5f);
                    position = "z-";
                }
            }
            Global.trapP.SetActive(true);
            Global.trapP.GetComponent<Image>().color = new Color(1,0,0,0.5f);
            Global.isPaused = true;
            Debug.Log("panel de la trampa activado");
            /*if(other0.transform.position.x < transform.position.x || other0.transform.position.z < transform.position.z)
            {
                if(transform.position.x - other0.transform.position.x > transform.position.z - other0.transform.position.z)
                    other0.transform.position = new Vector3(other0.transform.position.x + 1.5f, other0.transform.position.y, other0.transform.position.z);
                else
                    other0.transform.position = new Vector3(other0.transform.position.x, other0.transform.position.y, other0.transform.position.z + 1.5f);
            }
            else
            {
                if(other0.transform.position.x - transform.position.x  > other0.transform.position.z - transform.position.z)
                    other0.transform.position = new Vector3(other0.transform.position.x - 1.5f, other0.transform.position.y, other0.transform.position.z);
                else
                    other0.transform.position = new Vector3(other0.transform.position.x, other0.transform.position.y, other0.transform.position.z - 1.5f);
            }*/
            switch(rnd)
            {
                case 0:
                    if(other0.character.health >= 20f)
                    {
                        other0.character.health -= 20f;
                        trampText.text = "Has caido en una trampa: Tu vida se ha reducido en 20";
                    }
                    else
                    {
                        other0.character.health = 0f;
                        trampText.text = "Has caido en una trampa: Tu vida se ha reducido a 0";
                    }
                    break;
                case 1:
                    Global.onEndTurn = true;
                    trampText.text = "Has caido en una trampa: Tu turno termina";
                    break;
                case 2:
                    trampText.text = "Has caido en una trampa: Vuelves al inicio";
                    other0.transform.position = new Vector3(other0.character.initialX,other.transform.position.y,other0.character.initialZ);
                    break;
            }
        }
    }
    public void ButtonAceptar()
    {
        Global.trapP.SetActive(false);
        Global.isPaused = false;
    }
}
