using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [Header("Player Components")]
    Rigidbody2D rb;
    Animator anim;

    [Header("Movement Vector Control")]
    Vector2 lastMoveDirection;
    Vector2 movementDirection;
    Vector2 input;

    [Header("Player Control")]
    public float speed = 1.0f; // the movement speed of the player

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //HandleMovement();
        ProccessInputs();
        Animate();
    }

    void FixedUpdate()
    {
        if (input != Vector2.zero)
        {
            rb.MovePosition(rb.position + input * speed * Time.fixedDeltaTime);
        }
    }

    void ProccessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if ((moveX == 0 && moveY == 0) && (input.x != 0 || input.y != 0))
        {
            lastMoveDirection = input;
        }

        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        input = new Vector2(moveX, moveY).normalized;
    }

    void Animate()
    {
        anim.SetFloat("MoveX", input.x);
        anim.SetFloat("MoveY", input.y);
        anim.SetFloat("MoveMagnitude", input.magnitude);
        anim.SetFloat("LastMoveX", lastMoveDirection.x);
        anim.SetFloat("LastMoveY", lastMoveDirection.y);
    }

    /*void HandleMovement()
    {
        float horizontal = 0f;
        float vertical = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            vertical = 1f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            vertical = -1f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            horizontal = 1f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            horizontal = -1f;
        }

        movementDirection = new Vector2(horizontal, vertical).normalized;
    } */
}
