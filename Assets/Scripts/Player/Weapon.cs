using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Settings")]
    public float damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        enemy?.TakeDamage(damage); // mesma coisa de escrever if (enemy != null) {enemy.TakeDamage(damage)}
    }
}
