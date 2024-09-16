using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [Header("Behaviour Settings")]
    [SerializeField] private Transform travelPoint;
    [SerializeField] private float speed;
    [SerializeField] private float detectRad;

    private bool isAggro = false;
    private bool isForwards = false;
    private Vector3 startPos;
    private short direction;
    private Vector3 playerPos;
    private Rigidbody2D rb;
    private bool isActive = false;
    private HealthManager health;
    private SpriteRenderer sr;
    private BoxCollider2D bc;
    private DamageManager damager;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<HealthManager>();
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
        damager = GetComponent<DamageManager>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (health.isDead())
        {
            isActive = false;
            sr.enabled = false;
            bc.enabled = false;
        }
        if (isActive && !damager.isHit)
        {
            if (isPlayerInRange())
            {          
                transform.position = Vector2.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);
            }
            else
            {
                if (isForwards)
                {
                    if(transform.position != travelPoint.position)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, travelPoint.position, speed * Time.deltaTime);
                    }
                    else
                    {
                        isForwards = !isForwards;
                    }
                }
                else
                {
                    if (transform.position != startPos)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, startPos, speed * Time.deltaTime);
                    }
                    else
                    {
                        isForwards = !isForwards;
                    }
                
                }
            }
        }
    }

    bool isPlayerInRange()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, detectRad);
        foreach(Collider2D col in cols)
        {
            if (col.gameObject.CompareTag("Player")) { 
                playerPos = col.gameObject.transform.position;
                return true;
            }
        }
        return false;
    }

    private void OnBecameVisible()
    {
        isActive = true;
        sr.enabled = true;
        bc.enabled = true;
    }

    private void OnBecameInvisible()
    {
        isActive = false;
        transform.position = startPos;
        health.Reset();
        bc.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null) {
            if (collision.gameObject.CompareTag("Player"))
            {
               
            }
        }
    }
}
