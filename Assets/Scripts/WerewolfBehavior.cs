using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WerewolfBehavior : MonoBehaviour
{
    [Header("Behaviour Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float chargeCoolDown;
    [SerializeField] private float offset;


    private GameObject player;
    private float chargeTimer;
    private HealthManager health;
    private DamageManager damager;
    private Vector2 targetPos;
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<HealthManager>();
        damager = GetComponent<DamageManager>();
        chargeTimer = chargeCoolDown;
        player = GameObject.FindGameObjectWithTag("Player");
        targetPos = player.transform.position + (transform.position - player.transform.position).normalized * offset;
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
            if(chargeTimer <= 0)
            {
                    transform.position = Vector2.MoveTowards(transform.position, targetPos , speed * Time.deltaTime);
                    if(Vector2.Distance(transform.position, targetPos) < 0.01)
                    {
                        chargeTimer = chargeCoolDown;
                    }
                
            }
            else
            {
                chargeTimer -= Time.deltaTime;
                targetPos = player.transform.position + (transform.position - player.transform.position).normalized * offset;
            }

            
        }
    }



}
