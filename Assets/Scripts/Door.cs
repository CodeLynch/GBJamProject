using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Door : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private string doorName;
    [SerializeField] private string[] lockedDialogue;
    [SerializeField] private string[] openDialogue;
    [SerializeField] private GameObject dialogueBox;

    private DialogueScript dialogueContent;

    private void Start()
    {
        dialogueContent = dialogueBox.GetComponent<DialogueScript>();
        
    }
    public void Open()
    {
        if (doorName == "omega")
        {
            lockedDialogue = new string[] { "Scott: Cant leave yet...", "my friends are still here!", GameManager.instance.getRescueCount() + " out of 4 friends rescued" };
        }
        if (GameManager.instance.checkKey(doorName))
        {
            dialogueContent.setLines(openDialogue);
            dialogueBox.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            dialogueContent.setLines(lockedDialogue);
            dialogueBox.SetActive(true);
        }
    }

}
