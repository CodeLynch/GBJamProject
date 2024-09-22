using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverHandler : MonoBehaviour
{
    [Header("Choices")]
    [SerializeField] private GameObject continueTxt;
    [SerializeField] private GameObject exitTxt;
    [SerializeField] private float delay;
    //0 for continue
    //1 for exit
    private int choice = 0;
    private bool isSwitching = false;
    private TextMeshProUGUI continueTMP;
    private TextMeshProUGUI exitTMP;
    private void Start()
    {
        continueTMP = continueTxt.GetComponent<TextMeshProUGUI>();
        exitTMP = exitTxt.GetComponent<TextMeshProUGUI>();
    }
    // Update is called once per frame
    void Update()
    {
        if(choice == 0)
        {
            continueTMP.color = new Color(.9137256f, .4352942f, 0.1058824f);
            exitTMP.color = new Color(1, 0.9647059f, 0.8235295f);
        }
        else
        {
            exitTMP.color = new Color(.9137256f, .4352942f, 0.1058824f);
            continueTMP.color = new Color(1, 0.9647059f, 0.8235295f);
        }
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            if (!isSwitching)
            {
                StartCoroutine(ChoiceSwitch());
            }
        }

        else if (Input.GetKeyDown(KeyCode.Return))
        {
            if(choice == 0)
            {
                SceneManager.LoadScene("MainScene");
            }
            else
            {
                SceneManager.LoadScene("TitleScreen");
            }
        }
    }

    private IEnumerator ChoiceSwitch()
    {
        isSwitching = true;
        if (choice == 0) {
            choice = 1;
        }
        else
        {
            choice = 0;
        }
        yield return new WaitForSeconds(delay);
        isSwitching = false;
    }
           

}
