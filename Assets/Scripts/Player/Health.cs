using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health settings")]
    public float maxHealth = 10;
    public float currentHealth;

    [Header("INVENCIVEL")]
    public float invulnerabilityDuration = 1.0f;
    public bool isInvulnerable = false;

    [Header("Componentes")]
    SpriteRenderer spriteRenderer;
    PlayerController playerController;
    Animator playerAnim;

    public GameObject deathScreen;

    private void Start()
    {
        currentHealth = maxHealth; // seta a vida atual para que se torne a vida maxima
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerController = GetComponent<PlayerController>();
        playerAnim = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        if (isInvulnerable || currentHealth <= 0)
        {
            return;
        }

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvulnerabilityRoutine());
        }
    }

    IEnumerator InvulnerabilityRoutine()
    {
        isInvulnerable = true;
        float elapsed = 0f;

        while (elapsed < invulnerabilityDuration)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled; // "animação" do player piscando
            yield return new WaitForSeconds(0.1f);
            elapsed += 0.1f;
        }

        spriteRenderer.enabled = true;
        isInvulnerable = false;
    }

    void Die()
    {
        if (playerController != null)
        {
            playerController.enabled = false;
        }

        Shoot shootScript = GetComponent<Shoot>();
        if (shootScript != null)
        {
            shootScript.enabled = false;
        }

        deathScreen.SetActive(true);
        playerAnim.Play("Die");
        Debug.Log("morreu palhaço");
    }
}
