using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public string team = "";
    public int maxHealth = 3;
    public int health = 3;
    void Start()
    {
        health = maxHealth;
        this.gameObject.tag = "healthbody";
    }

    public int TakeDamage(int amount)
    {
        health -= amount;
        if(amount > health)
        {
            health = 0;
            return amount - health;
        }
        else
        {
            health -= amount;
            return 0;
        }
    }
}
