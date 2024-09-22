using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropFood : MonoBehaviour
{
    private HealthManager health;
    public Food[] foods = new Food[4];
    void Start()
    {
        health = GetComponent<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health.getHealth() <= 0)
        {
            int rng = (int) Mathf.Ceil(Random.Range(1, 100));
            if (rng > 80)
            {
                if(rng < 85)
                {
                    Instantiate(foods[0].food, transform.position, transform.rotation);
                }
                else if (rng < 90) {
                    Instantiate(foods[1].food, transform.position, transform.rotation);
                }
                else if (rng < 99)
                {
                    Instantiate(foods[2].food, transform.position, transform.rotation);
                }
                else
                {
                     Instantiate(foods[3].food, transform.position, transform.rotation);
                }
            }
        }
    }
}
[System.Serializable]
public class Food
{
    public GameObject food;
}
