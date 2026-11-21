using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Components")]
    Rigidbody2D rb;
    Animator anim;
    Camera mainCam;

    [Header("Movement and Aim Control")]
    Vector2 lastMoveDirection;
    Vector2 input;
    Vector2 aimDirection;

    [Header("Player Control")]
    public float speed = 1.0f;
    public Transform aim;

    // Propriedade pública para o script Shoot saber se o player está andando
    public bool IsWalking { get; private set; } = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        mainCam = Camera.main;
    }

    void Update()
    {
        ProccessInputs();
        ProccessAim();
        Animate();
    }

    void FixedUpdate()
    {
        if (input != Vector2.zero)
        {
            rb.MovePosition(rb.position + input * speed * Time.fixedDeltaTime);
        }

        // Se estiver andando, o aim segue o teclado; se estiver parado, segue o mouse
        Vector2 lookTarget = IsWalking ? input : aimDirection;

        if (lookTarget != Vector2.zero)
        {
            Vector3 vector3 = Vector3.left * lookTarget.x + Vector3.down * lookTarget.y;
            aim.rotation = Quaternion.LookRotation(Vector3.forward, vector3);
        }
    }

    void ProccessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if ((moveX == 0 && moveY == 0) && (input.x != 0 || input.y != 0))
        {
            IsWalking = false;
            lastMoveDirection = input;
        }
        else if (moveX != 0 || moveY != 0)
        {
            IsWalking = true;
        }

        input = new Vector2(moveX, moveY).normalized;
    }

    void ProccessAim()
    {
        // Só mira com o mouse enquanto estiver parado
        if (!IsWalking)
        {
            Vector3 mouseScreenPos = Input.mousePosition;
            mouseScreenPos.z = Mathf.Abs(mainCam.transform.position.z);
            Vector2 mouseWorldPos = mainCam.ScreenToWorldPoint(mouseScreenPos);

            aimDirection = (mouseWorldPos - (Vector2)transform.position).normalized;
            lastMoveDirection = aimDirection;
        }
    }

    void Animate()
    {
        anim.SetFloat("MoveX", input.x);
        anim.SetFloat("MoveY", input.y);
        anim.SetFloat("MoveMagnitude", input.magnitude);
        anim.SetFloat("LastMoveX", lastMoveDirection.x);
        anim.SetFloat("LastMoveY", lastMoveDirection.y);
    }
}