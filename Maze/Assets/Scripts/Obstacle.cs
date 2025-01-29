using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class Obstacle : MonoBehaviour
{
    public string type;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            MovPlayer1 other0 = other.gameObject.GetComponent<MovPlayer1>();
            if(other0.character.ability == Abilities.teleport)
            {
                if(other0.character.AbilityIsActive())
                {
                    Jump((int)Math.Floor(transform.position.x), (int)Math.Floor(transform.position.z), other0);
                }
                else
                {
                    Global.isPaused = true;
                    Global.trapP.GetComponent<Image>().color = new Color(0,0,0,0.5f);
                    Global.trapP.SetActive(true);
                    Global.trapT.text = "Usa tu habilidad para cruzar";
                    Push((int)Math.Floor(transform.position.x), (int)Math.Floor(transform.position.z), other0);
                }
            }
            else
            {
                if (other0.keys.Contains(type))
                {
                    Global.isPaused = true;
                    Global.trapP.GetComponent<Image>().color = new Color(0,1,0,0.5f);
                    Global.trapP.SetActive(true);
                    Global.trapT.text = "Tenias la llave de esta puerta, la has abierto"; //poner boton de abrir puerta
                    Destroy(gameObject);
                    other0.keys.Remove(type);
                    switch(type)
                    {
                        case "type0":
                            other0.character.countKey0 --;
                            break;
                        case "type1":
                            other0.character.countKey1 --;
                            break;
                        case "type2":
                            other0.character.countKey2 --;
                            break;
                    }                    
                }
                else 
                {
                    Global.isPaused = true;
                    Global.trapP.GetComponent<Image>().color = new Color(0,0,0,0.5f);
                    Global.trapP.SetActive(true);
                    Global.trapT.text = $"No tienes ninguna llave de {type} que pueda abrir esta puerta";
                }
            }
        }
    }

    void Jump(int x, int z, MovPlayer1 other0)
    {
        if(Global.maze.mazee[x,z].modo == "vertical")
        {
            if(other0.transform.position.x > x)
            {
                other0.transform.position = new Vector3(other0. transform.position.x - 1.5f, other0.transform.position.y, other0.transform.position.z);
            }
            else
            {
                other0.transform.position = new Vector3(other0. transform.position.x + 1.5f, other0.transform.position.y, other0.transform.position.z);
            }            
        }
        else
        {
            if(other0.transform.position.z > z)
            {
                other0.transform.position = new Vector3(other0. transform.position.x, other0.transform.position.y, other0.transform.position.z - 1.5f);
            }
            else
            {
                other0.transform.position = new Vector3(other0. transform.position.x, other0.transform.position.y, other0.transform.position.z + 1.5f);
            }
        }
    }

    void Push(int x, int z, MovPlayer1 other0)
    {
        if(Global.maze.mazee[x,z].modo == "vertical")
        {
            if(other0.transform.position.x > x)
            {
                other0.transform.position = new Vector3(other0. transform.position.x + 0.6f, other0.transform.position.y, other0.transform.position.z);
            }
            else
            {
                other0.transform.position = new Vector3(other0. transform.position.x - 0.6f, other0.transform.position.y, other0.transform.position.z);
            }            
        }
        else
        {
            if(other0.transform.position.z > z)
            {
                other0.transform.position = new Vector3(other0. transform.position.x, other0.transform.position.y, other0.transform.position.z + 0.6f);
            }
            else
            {
                other0.transform.position = new Vector3(other0. transform.position.x, other0.transform.position.y, other0.transform.position.z - 0.6f);
            }
        }
    }
}
