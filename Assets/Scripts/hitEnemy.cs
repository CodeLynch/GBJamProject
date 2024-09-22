using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitEnemy : MonoBehaviour
{
    [SerializeField] private int damage;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                DamageManager damager = collision.gameObject.GetComponent<DamageManager>();
                damager.Hit((collision.gameObject.transform.position - transform.position).normalized, damage);    
            }
        }
    }
    
}
