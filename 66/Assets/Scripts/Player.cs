using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movimento")]
    [SerializeField] private float speed;

    private Vector2 direction;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Collider2D col;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    private void Start()
    {
        rb.freezeRotation = true;
    }

    private void Update()
    {
        ReadInput();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    private void ReadInput()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (direction.x < 0)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        else if (direction.x > 0)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
    }

    
    private void ApplyMovement()
    {
        rb.linearVelocity = direction.normalized * speed;
    }
}
