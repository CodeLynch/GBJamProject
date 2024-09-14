using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class areaTrigger : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Vector2 newPos;
    [SerializeField] private float TransitionDuration = 1f;
    Camera cam;
    Vector3 newPos3;
    private bool isPanning = false;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        newPos3 = new Vector3(newPos.x, newPos.y, cam.transform.position.z);
    }

    

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            if (cam.transform.position != newPos3)
            {
                StartCoroutine(camTransition());
            }
        }
    }

    private IEnumerator camTransition()
    {
        float t = 0f;
        Time.timeScale = 0f;
        Vector3 oldPos = cam.transform.position;
        while(t < 1.0f)
        {
            t += Time.unscaledDeltaTime * (1 / TransitionDuration);
            cam.transform.position = Vector3.Lerp(oldPos, newPos3, t);
            yield return 0;
        }
        Time.timeScale = 1f; 
    }
}
