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

    public void Reset()
    {
        health = orig;
    }
    public bool isDead()
    {
        return health <= 0;
    }

    
}
