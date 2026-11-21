using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float speed = 1.0f; // the movement speed of the player
    private Rigidbody2D rb;
    private Vector2 movementDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleMovement();
    }
    
    void FixedUpdate()
    {
        if (movementDirection != Vector2.zero)
        {
            rb.MovePosition(rb.position + movementDirection * speed * Time.fixedDeltaTime);
        }

        FindAnyObjectByType<PlayerAnimation>().SetDirection(movementDirection);
    }

    void HandleMovement()
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
    }
}
