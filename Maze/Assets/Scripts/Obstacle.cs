using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class Obstacle : MonoBehaviour
{
    public string type;
    private GameObject panel = Global.trapP;
    private TMP_Text text = Global.trapT;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit the obstacle");
            MovPlayer1 other0 = other.gameObject.GetComponent<MovPlayer1>();
            if(other0.character.ability == Abilities.teleport)
            {
                if(other0.character.AbilityIsActive())
                {
                    Debug.Log("activaste tu habilidad");
                    Jump((int)Math.Floor(transform.position.x), (int)Math.Floor(transform.position.z), other0);
                }
                else
                {
                    Global.isPaused = true;
                    panel.GetComponent<Image>().color = new Color(0,0,0,0.5f);
                    panel.SetActive(true);
                    text.text = "Usa tu habilidad para cruzar";
                    Push((int)Math.Floor(transform.position.x), (int)Math.Floor(transform.position.z), other0);
                }
            }
            else
            {
                if (other0.keys.Contains(type))
                {
                    Global.isPaused = true;
                    panel.GetComponent<Image>().color = new Color(0,1,0,0.5f);
                    panel.SetActive(true);
                    text.text = "Tenias la llave de esta puerta, la has abierto"; //poner boton de abrir puerta
                    Debug.Log("Tenias la llave, has pasado");
                    transform.localScale=new Vector3(0,0,0);
                    other0.keys.Remove(type);
                }
                else 
                {
                    Debug.Log("Busca una llave de " + type);
                    Global.isPaused = true;
                    panel.GetComponent<Image>().color = new Color(0,0,0,0.5f);
                    panel.SetActive(true);
                    text.text = $"No tienes ninguna llave de {type} que pueda abrir esta puerta";
                }
                Debug.Log(other0.character.skill);
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
