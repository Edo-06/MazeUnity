using System;
using UnityEngine;
using System.Collections.Generic;

public enum Abilities
{
    trapDetector,
    boom,
    heal,
    atack,
    teleport,
    frezee,
    invisibility,
    poison,
    bless,
    curse,
    enhancedMemory,
    //dash,

}

public class Character
{
    public float health, speed, maxHealth;
    public int initialX, initialZ;
    public List<int[]> playerTrap = new List<int[]>();
    public string skill;
    public Abilities ability;
    public float abilityCooldown;
    public float abilityActiveDuration;
    public float currentCooldown;
    public float currentActiveTime;
    public List<int[]> playerTrapTemp = new List<int[]>();
    public float turnDuration;
    public bool poisoned = false;
    
    public Character(float health, float maxHealth, float speed, string skill, float turnDuration, int initialX = 0, int initialZ = 0)
    {
        this.health = health;
        this.speed = speed;
        this.skill = skill;
        this.maxHealth = maxHealth;
        this.turnDuration = turnDuration;
        this.initialX = initialX;
        this.initialZ = initialZ;
        currentCooldown = abilityCooldown;
        currentActiveTime = 0;
    }

    public bool IsDeath()
    {
        return health < 2f;
    }
    public bool AbilityIsActive()
    {
        return currentActiveTime > 0;
    }

    public void SetAbility(Abilities ability, float cooldown, float activeDuration)
    {
        this.ability = ability;
        abilityCooldown = cooldown;
        abilityActiveDuration = activeDuration;
        currentCooldown = cooldown;
    }

    public void UseAbility()
    {
        if (currentCooldown >= abilityCooldown)
        {
            Debug.Log($"Using ability: {ability}");
            currentActiveTime = abilityActiveDuration;
            // lo que hace la habilidad
            switch(ability)
            {
                case Abilities.trapDetector:
                    playerTrapTemp = playerTrap;
                    playerTrap = Global.allTheTraps;
                    break;
                case Abilities.heal:
                    health +=10f;
                    break;
                case Abilities.boom:
                    break;
                case Abilities.enhancedMemory:
                    break;
                case Abilities.teleport:
                    break;
            }
        }
        else
        {
            Debug.Log($"{ability} is on cooldown");
        }
    }

    public void UpdateCooldowns(float deltaTime)
    {
        if(!Global.isPaused)
        {
            if (currentCooldown <= abilityCooldown && currentActiveTime <= 0)
            {
                currentCooldown += deltaTime;
            }
            if (currentActiveTime > 0)
            {
                currentActiveTime -= deltaTime;
                if (currentActiveTime <= 0)
                {
                    Debug.Log($"Ability {ability} has ended");
                    currentCooldown = 0;
                    // terminar
                }
            }
        }
        
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health < 0)
        {
            health = 0;
        }
    }
    public void Heal(float amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }


}
