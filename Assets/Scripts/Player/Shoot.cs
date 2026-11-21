using UnityEngine;

public class Shoot : MonoBehaviour
{
    [Header("Components")]
    public Transform aim;
    public GameObject bullet;
    PlayerController playerController;

    [Header("Shoot Settings")]
    public float fireForce = 10f;
    float shootCooldown = 0.25f;
    float shootTimer = 0.5f;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        shootTimer += Time.deltaTime;

        if (Input.GetMouseButton(0) && !playerController.IsWalking)
        {
            OnShoot();
        }
    }

    void OnShoot()
    {
        if (shootTimer > shootCooldown)
        {
            shootTimer = 0;
            GameObject intBullet = Instantiate(bullet, aim.position, aim.rotation);
            intBullet.GetComponent<Rigidbody2D>().AddForce(-aim.up * fireForce, ForceMode2D.Impulse);
            Destroy(intBullet, 2f);
        }
    }
}
