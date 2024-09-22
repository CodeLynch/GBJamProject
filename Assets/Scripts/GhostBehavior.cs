using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBehavior : MonoBehaviour
{
    [Header("Behaviour Settings")]
    public Transform travelPoint;
    [SerializeField] private float maxSpeed;

    private bool isForwards = true;
    private Vector3 startPos;
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        speed = Random.Range(1, maxSpeed);
    }

    // Update is called once per frame
    void LateUpdate()
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
