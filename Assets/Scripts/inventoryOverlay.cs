using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class inventoryOverlay : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float sliderDuration = 0.5f;
    [SerializeField] private GameObject face;
    [SerializeField] private GameObject bpm;
    [SerializeField] private GameObject player;
    [SerializeField] private TextMeshProUGUI hp;

    private bool isOpeningInv;
    private RectTransform panelTransform;
    private bool isOpen;
    private MoodManager moodSetter;
    private HealthManager playerHp;
    private Animator bpmSpeed;
   
    // Start is called before the first frame update
    void Start()
    {
        panelTransform = GetComponent<RectTransform>();
        moodSetter = face.GetComponent<MoodManager>(); 
        playerHp = player.GetComponent<HealthManager>();
        bpmSpeed = bpm.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen)
        {
            hp.text = "HP: " + playerHp.getHealth().ToString();
            if(playerHp.getHealth() >= 50)
            {
                bpmSpeed.SetFloat("speedMult", 1);
                bpmSpeed.SetBool("isWeak", false);
                moodSetter.setMood("healthy");
            }else if(playerHp.getHealth() < 50)
            {
                bpmSpeed.SetBool("isWeak", true);
                moodSetter.setMood("angry");
            }else if(playerHp.getHealth() <= 20)
            {
                bpmSpeed.SetFloat("speedMult", 2f);
            }
            else if (playerHp.getHealth() <= 10)
            {
                bpmSpeed.SetFloat("speedMult", 3f);
            }




            //Time.timeScale = 0f;
            if (Input.GetKeyDown(KeyCode.Return) && !isOpeningInv)
            {
                
                StartCoroutine(SlideUp(false));
                isOpen = !isOpen;

            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Return) && !isOpeningInv && Time.timeScale == 1 && playerHp.getHealth() > 0)
            {
                InventoryManager.instance.ListItems();
                StartCoroutine(SlideUp(true));
                isOpen = !isOpen;
            }

        }
    }

    private IEnumerator SlideUp(bool up)
    {
        isOpeningInv = true;
        Time.timeScale = 0f;
        float t = 0f;
        switch (up)
        {
            case true:
                while (t < 1.0f)
                {
                    t += Time.unscaledDeltaTime * (1 / sliderDuration);
                    panelTransform.offsetMax = new Vector2(panelTransform.offsetMax.x, Mathf.Lerp(-170, 0, t));
                    panelTransform.offsetMin = new Vector2(panelTransform.offsetMin.x, Mathf.Lerp(-170, 0, t));
                        yield return 0;
                }
                break;
            case false:
                while (t < 1.0f)
                {
                    t += Time.unscaledDeltaTime * (1 / sliderDuration);
                    panelTransform.offsetMax = new Vector2(panelTransform.offsetMax.x, Mathf.Lerp(0, -170, t));
                    panelTransform.offsetMin = new Vector2(panelTransform.offsetMin.x, Mathf.Lerp(0, -170, t));
                    yield return 0;
                }
                break;
        }
        if (!isOpen)
        {
            Time.timeScale = 1f;
        }
        isOpeningInv = false;
    }
}
