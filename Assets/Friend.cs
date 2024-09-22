using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friend : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private string[] dialogue;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private AnimationClip up;
    [SerializeField] private AnimationClip horizontal;
    [SerializeField] private AnimationClip down;

    private DialogueScript dialogueContent;
    private bool rescued = false;
    private GameObject player;
    private Animator anim;
    private string currentAnim;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        dialogueContent = dialogueBox.GetComponent<DialogueScript>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (rescued && !dialogueBox.activeSelf)
        {
            Destroy(gameObject);
        }
    }
    public void Talk()
    {
        Vector2 Point_1 = transform.position;
        Vector2 Point_2 = player.transform.position;
        float angle = Mathf.Atan2(Point_2.y - Point_1.y, Point_2.x - Point_1.x) * 180 / Mathf.PI;
        if (angle > -45 && angle < 45) {

            changeAnim(horizontal.name);
            transform.localScale = new Vector2(-1, 1);
        }
        else if (angle > 45 && angle < 145)
        {
            changeAnim(up.name);
        }
        else if (angle > 145 && angle < -145)
        {
            changeAnim(horizontal.name);
            transform.localScale = new Vector2(-1, 1);
        }
        else if (angle > -145 && angle < -45)
        {
            changeAnim(down.name);
        }
        dialogueContent.setLines(dialogue);
        dialogueBox.SetActive(true);
        GameManager.instance.setRescueCount(GameManager.instance.getRescueCount() + 1);
        rescued = true;
    }

    private void changeAnim(string state)
    {
        if (currentAnim == state)
        {
            return;
        }
        else
        {
            anim.Play(state);
            currentAnim = state;
        }
    }
}
