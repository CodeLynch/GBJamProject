using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public GameObject explosion;
    private HealthManager healthManager;
    // Start is called before the first frame update
    void Start()
    {
        healthManager = GetComponent<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (healthManager.isDead())
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }
    }
}
