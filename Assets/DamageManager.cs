using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    [Header("Props")]
    [SerializeField] private float knockBack;
    [SerializeField] private float knockBackDuration;

    private HealthManager health;
    private Rigidbody2D rb;

    public bool isHit = false;

    private void Start()
    {
        health = GetComponent<HealthManager>();
        rb = GetComponent<Rigidbody2D>();
    }
    public void Hit(Vector2 direction, int damage)
    {
        health.Damage(damage);
        Vector2 knockBackDir = direction.normalized * knockBack;
        if (!isHit)
        {
            StartCoroutine(getKnockedback(knockBackDir));
        }
    }

    private IEnumerator getKnockedback(Vector2 direction)
    {
        isHit = true;
        rb.AddForce(direction, ForceMode2D.Impulse);
        yield return new WaitForSeconds(knockBackDuration);
        isHit = false;
    }
}
