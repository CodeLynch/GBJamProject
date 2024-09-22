using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyProjectile : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float speed;
    
    private Rigidbody2D rb;
    private Vector3 target;
    private GameObject player;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        target = (player.transform.position - transform.position).normalized;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(target * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);

    }
}
