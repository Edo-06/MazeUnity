using System;
using UnityEngine;

public class Character
{
    public float health, speed;
    public string skill;
    public Character(float health, float speed, string skill)
    {
        this.health = health;
        this.speed = speed;
        this.skill = skill;
    }
}
