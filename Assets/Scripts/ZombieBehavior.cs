using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehavior : MonoBehaviour
{
    [Header("Behaviour Settings")]
    public Transform travelPoint;
    [SerializeField] private float speed;
    [SerializeField] private float detectRad;

    private bool isForwards = true;
    private Vector3 startPos;
    private Vector3 playerPos;
    private Rigidbody2D rb;
    private HealthManager health;
    private DamageManager damager;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<HealthManager>();
        damager = GetComponent<DamageManager>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (health.isDead())
        {
            Destroy(gameObject);
        }
        
        if (!damager.isHit)
        {
            if (isPlayerInRange())
            {          
                transform.position = Vector2.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);
            }
            else
            {
                if (isForwards)
                {
                    if (Vector2.Distance(transform.position, travelPoint.position) < 0.01)
                    {
                        isForwards = !isForwards;
                    }
                    else if(transform.position != travelPoint.position)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, travelPoint.position, speed * Time.deltaTime);
                    }
                }
                else
                {
                    if(Vector2.Distance(transform.position, startPos) < 0.01)
                    {
                        isForwards = !isForwards;
                    }
                    else if (transform.position != startPos)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, startPos, speed * Time.deltaTime);
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

}
