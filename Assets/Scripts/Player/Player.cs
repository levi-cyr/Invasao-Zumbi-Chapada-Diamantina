using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject winScreen;
    public GameManager gameManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Win") && gameManager.canWin)
        {
            winScreen.SetActive(true);
        }
    }
}
