using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class areaManager : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private List<Enemy> enemies = new();
    private bool spawned = false;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 screenPos = cam.WorldToScreenPoint(transform.position);
        if((screenPos.x > 0 && screenPos.x < cam.pixelWidth) && (screenPos.y > 0 && screenPos.y < cam.pixelHeight))
        {
            if (!spawned)
            {
                activateList();
                spawned = true;
            }
        }
        else
        {
            if (spawned)
            {
                clearList();
                spawned = false;
            }
        }  
    }

    void activateList()
    {
        foreach(Enemy enemy in enemies)
        {
            enemy.instance = Instantiate(enemy.enemy, enemy.spawnPoint.position, transform.rotation);
            enemy.instance.GetComponent<EnemyBehavior>().travelPoint = enemy.travelPoint;
        }
    }

    void clearList()
    {
        foreach(Enemy enemy in enemies)
        {
            Destroy(enemy.instance);
        }
    }
}

[System.Serializable]
public class Enemy
{
    public GameObject enemy;
    public Transform spawnPoint;
    public Transform travelPoint;
    public GameObject instance;
}