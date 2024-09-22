using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodObj : MonoBehaviour
{
    [Header("Attributes")]
    public int val;

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

