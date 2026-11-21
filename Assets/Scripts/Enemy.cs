using System;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour
{
    public static event Action OnEnemyKilled;

    [Header("Enemy Settings")]
    public float health = 3f;
    public float speed = 0.4f; // velocidade do inimigo (zumbi)

    [Header("Enemy Components")]
    Rigidbody2D rb; // rigidbody do inimigo
    public Transform target; // alvo do inimigo
    Vector2 moveDirection;

    bool isDead = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        target = GameObject.Find("Player").transform; // caça o gameobject do player
    }

    void Update()
    {
        if (target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            moveDirection = direction;
        }
    }

    void FixedUpdate()
    {
        if (target)
        {
            rb.linearVelocity = new Vector2(moveDirection.x, moveDirection.y) * speed;
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        health -= damage;
        if (health <= 0)
        {
            isDead = true;
            OnEnemyKilled?.Invoke();
            Destroy(gameObject); //mata o zombie
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            playerHealth?.TakeDamage(1);
        }
    }
}
