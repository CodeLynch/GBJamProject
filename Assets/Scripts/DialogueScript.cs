using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueScript : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Properties")]
    [SerializeField] TextMeshProUGUI textComp;
    [SerializeField] private string[] lines;
    [SerializeField] private float textSpeed;


    private int index;
    private bool restart = false;
    void Start()
    {
        textComp.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (restart)
        {
            StartDialogue();
            restart = false;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                if(textComp.text == lines[index])
                {
                    NextLine();
                }
                else
                {
                    StopAllCoroutines();
                    textComp.text = lines[index];
                }
            }
        }
    }
    public void setLines(string[] lines)
    {
        this.lines = lines;
    }

    void StartDialogue()
    {
        index = 0;
        Time.timeScale = 0f;
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        foreach(char c in lines[index].ToCharArray())
        {
            textComp.text += c;
            yield return new WaitForSecondsRealtime(textSpeed);
        }
    }

    void NextLine()
    {
        if(index < lines.Length - 1)
        {
            index++;
            textComp.text = string.Empty;
            StartCoroutine(TypeText());
        }
        else
        {
            index = 0;
            Time.timeScale = 1;
            textComp.text = string.Empty;
            restart = true;
            gameObject.SetActive(false);
        }
    }
}
