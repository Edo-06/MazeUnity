using System.Data;
using UnityEngine;
using UnityEngine.UIElements;


public class Health : MonoBehaviour
{
    public Slider healthSlider;
    public float maxHealth = 100f;
    private float currentHealth;
        public void Bar()
    {
        healthSlider.value = currentHealth;
    }
}
