using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] private GameObject player;

    [Header("Distance")]
    [SerializeField] private float distanceWakeUp;
    [SerializeField] private float distanceAttack;

    [Header("Att")]
    [SerializeField] private float speed;
    

    private Animator animator;
    private SpriteRenderer spriteRender;

    private bool wakeUp;
    private bool attacking;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRender = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        wakeUp = false;
    }

    private void Update()
    {
        if (!wakeUp)
        {
            CheckWakeUp();
        }
        else
        {
            if (Vector2.Distance(transform.position, player.transform.position) < distanceAttack)
            {
                Attack();
            }
            else
            {
                Move();
            }
        }

        UpdateAnimator();
    }


    private void CheckWakeUp()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < distanceWakeUp)
        {
            wakeUp = true;
        }
    }

    
    private void Move()
    {
        if (attacking) return;

        transform.position = Vector2.MoveTowards(transform.position,
                                                   player.transform.position,
                                                   speed * Time.deltaTime);

        if (player.transform.position.x > transform.position.x)
        {
            spriteRender.flipX = false;
        }
        else if (player.transform.position.x < transform.position.x)
        {
            spriteRender.flipX = true;
        }
    }

    private void Attack()
    {
        StartCoroutine(Attacking());
    }

    private IEnumerator Attacking()
    {
        attacking = true;
        wakeUp = true;
        yield return new WaitForSeconds(1.5f);
        attacking = false;
    }


    private void UpdateAnimator()
    {
        animator.SetBool("pWakeUp", wakeUp);
        animator.SetBool("pAttack", attacking);
        
    }

}