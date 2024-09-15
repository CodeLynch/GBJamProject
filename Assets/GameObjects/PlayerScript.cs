using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float interactRaycastLength = 1f;

    //1 for top
    //2 for right
    //3 for bottom
    //4 for left
    [SerializeField]private short direction = 3;
    [SerializeField]private AnimationClip frontState;
    [SerializeField]private AnimationClip backState;
    [SerializeField]private AnimationClip horizontalState;

    private Rigidbody2D rb;
    private Animator anim;
    private string currentAnim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        #region movement
        if(Time.timeScale > 0)
        {
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                changeAnim(horizontalState.name);
                if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    direction = 2;
                    transform.localScale = new Vector2(1, 1);
                }
                else
                {
                    direction = 4;
                    transform.localScale = new Vector2(-1, 1);
                }

            }else if(Input.GetAxisRaw("Vertical") != 0)
            {
                if (Input.GetAxisRaw("Vertical") > 0)
                {
                    direction = 1;
                    changeAnim(backState.name);
                }
                else
                {
                    direction = 3;
                    changeAnim(frontState.name);
                }
            }
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * moveSpeed;
        }
        #endregion

        #region interaction
        if (Input.GetKeyDown(KeyCode.X))
        {
            Vector2 targetPos = Vector2.zero;
            switch (direction)
            {
                case 1: 
                    targetPos = Vector2.up; 
                    break;
                case 2:
                    targetPos = Vector2.right;
                    break;
                case 3:
                    targetPos = Vector2.down;
                    break;
                case 4:
                    targetPos = Vector2.left;
                    break;
                default:
                    break;
            }
            RaycastHit2D rayCheck = Physics2D.Raycast(transform.position, targetPos, interactRaycastLength);
            if (rayCheck.collider != null)
            {
                if (rayCheck.collider.CompareTag("Door"))
                {
                    Door doorComp = rayCheck.collider.gameObject.GetComponent<Door>();
                    doorComp.Open();
                }
            }
        }
        #endregion
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
            
        }
    }
}
