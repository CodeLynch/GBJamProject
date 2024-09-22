using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private string keyName;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private string[] GetDialogue;
    private DialogueScript dialogueContent;

    private void Start()
    {
        dialogueContent = dialogueBox.GetComponent<DialogueScript>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.getKey(keyName);
            dialogueContent.setLines(GetDialogue);
            InventoryManager.instance.items.Add( keyName[0].ToString().ToUpper() + keyName.Substring(1) + " Key");
            dialogueBox.SetActive(true);
            Destroy(gameObject);
        }
    }
}
