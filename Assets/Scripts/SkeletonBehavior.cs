using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBehavior : MonoBehaviour
{
    [Header("Behaviour Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float throwCoolDown;
    [SerializeField] private GameObject soda;
    [SerializeField] private float detectRad;


    private GameObject player;
    private float throwTimer;
    private HealthManager health;
    private DamageManager damager;
    private bool isThrowing;
    private Vector3 playerPos;
    private Camera cam;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        health = GetComponent<HealthManager>();
        damager = GetComponent<DamageManager>();
        throwTimer = 0;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        if (health.isDead())
        {
            Destroy(gameObject);
        }
        
        if (!damager.isHit && player != null)
        {
            if(throwTimer <= 0)
            {    
                if (!isThrowing)
                {
                    StartCoroutine(throwRoutine());
                }
            }
            else
            {
                throwTimer -= Time.deltaTime;
            }

            Vector2 screenPos = cam.WorldToScreenPoint(transform.position);
            if (isPlayerInRange() && (screenPos.x > 16 && screenPos.x < cam.pixelWidth - 16) && (screenPos.y > 16 && screenPos.y < cam.pixelHeight - 16))
            {
                rb.velocity = (transform.position - playerPos).normalized * speed;
            }else
            {
                
                rb.velocity = Vector2.zero;
            }


        }
    }

    private IEnumerator throwRoutine()
    {
        isThrowing = true;
        Instantiate(soda, transform.position, transform.rotation);
        yield return new WaitForSeconds(.7f);
        Instantiate(soda, transform.position, transform.rotation);
        yield return new WaitForSeconds(.7f);
        Instantiate(soda, transform.position, transform.rotation);
        isThrowing = false;
        throwTimer = throwCoolDown;

    }

    bool isPlayerInRange()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, detectRad);
        foreach (Collider2D col in cols)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                playerPos = col.gameObject.transform.position;
                return true;
            }
        }
        return false;
    }



}
