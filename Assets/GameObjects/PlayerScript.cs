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
    [SerializeField] private float knockBack;
    [SerializeField] private float knockBackDuration;
    [SerializeField] private int getHitAmt;
    [SerializeField] private float inviDuration;

    [Header("Animation States")]
    [SerializeField]private AnimationClip frontState;
    [SerializeField]private AnimationClip backState;
    [SerializeField]private AnimationClip horizontalState;
    [SerializeField] private AnimationClip frontAtkState;
    [SerializeField] private AnimationClip backAtkState;
    [SerializeField] private AnimationClip horizontalAtkState;

    private HealthManager health;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    private string currentAnim;
    private bool isAttack;
    private bool isMoving;
    private bool isHit;
    private bool isInvi;
    private float inviTimer;
    private bool coroutineStart;
  
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        health = GetComponent<HealthManager>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health.isDead())
        {
            Destroy(gameObject);
        }
        if(Time.timeScale > 0 && !isAttack)
        {
            #region movement
                if (Input.GetAxisRaw("Horizontal") != 0)
                {
                    isMoving = true;
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
                    isMoving = true;
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
                else
                {
                    isMoving = false;
                }
            if (!isHit)
            {
                rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * moveSpeed;
            }
            
            #endregion

            #region interaction
            if (Input.GetKeyDown(KeyCode.X) && !isAttack)
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

            #region attack
                if (Input.GetKeyDown(KeyCode.Z)) {
                    if (!isAttack) {
                        rb.velocity = Vector2.zero;
                        StartCoroutine(attack(direction));   
                    }
                }
            #endregion
        }

        if (isInvi)
        {
            if(inviTimer <= 0)
            {
                isInvi = false;
            }
            else
            {
                if (!coroutineStart)
                {
                    StartCoroutine(blink());
                }
                inviTimer -= Time.deltaTime;
            }
        }
        
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

    private IEnumerator attack(short dir)
    {
        isAttack = true;
        switch (dir)
        {
            case 1:
                
                changeAnim(backAtkState.name);
                yield return new WaitForSeconds(backAtkState.length);
                changeAnim(backState.name);
                isAttack = false;

                break;
            case 2:
                transform.localScale = new Vector2(1, 1);
                changeAnim(horizontalAtkState.name);
                yield return new WaitForSeconds(horizontalAtkState.length);
                changeAnim(horizontalState.name);
                isAttack = false;

                break;
            case 3:
                changeAnim(frontAtkState.name);
                yield return new WaitForSeconds(frontAtkState.length);
                changeAnim(frontState.name);
                isAttack = false;

                break;
            case 4:
                transform.localScale = new Vector2(-1, 1);
                changeAnim(horizontalAtkState.name);
                yield return new WaitForSeconds(horizontalAtkState.length);
                changeAnim(horizontalState.name);
                isAttack = false;
                break;
            default: break;
        }
    }

    private IEnumerator getKnockedback(Vector2 direction)
    {
        isHit = true;
        rb.AddForce(direction, ForceMode2D.Impulse);
        yield return new WaitForSeconds(knockBackDuration);
        isHit = false;
    }

    private IEnumerator blink()
    {
        coroutineStart = true;
        sr.enabled = false;
        yield return new WaitForSeconds(.07f);
        sr.enabled = true;
        coroutineStart = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null && !isInvi) {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                health.Damage(getHitAmt);
                Vector2 knockBackDir = (transform.position - collision.transform.position).normalized * knockBack;
                StartCoroutine(getKnockedback(knockBackDir));
                inviTimer = inviDuration;
                isInvi = true;
            }
        }
    }
}
