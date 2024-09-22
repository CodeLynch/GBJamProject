using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [Header("Props")]
    [SerializeField] private int health;
    private int orig;
    private void Start()
    {
        orig = health;
    }
    public void Damage(int val)
    {
        health -= val;
    }

    public void Heal(int val)
    {
        int healRes = health + val;
        if (healRes >= 100)
        {
            health = 100;
        }
        else
        {
            health += val;
        }
    }

    public int getHealth()
    {
        return health; 
    }
    public void Reset()
    {
        health = orig;
    }

    public bool isDead()
    {
        return health <= 0;
    }


    
}
