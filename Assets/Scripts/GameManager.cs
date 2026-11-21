using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TMP_Text timer;
    float timeLeft = 120f;
    public bool stopTime;

    [Header("Win condition")]
    public bool canWin = false;
    public int killCount = 0;
    public TMP_Text killCountText;

    private void OnEnable()
    {
        Enemy.OnEnemyKilled += AddKill;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyKilled -= AddKill;
    }

    void Start()
    {
        stopTime = false;
        UpdateKillUI();
    }

    void Update()
    {
        Timer();
    }

    void Timer()
    {
        if (!stopTime)
        {
            timeLeft = timeLeft - Time.deltaTime;
            timer.text = "Time left: " + timeLeft.ToString("F0");
        }

        if (timeLeft <= 0)
        {
            stopTime = true;
            timeLeft = 0;
            timer.text = "Time Left: 0";
        }
    }

    void AddKill()
    {
        killCount++;
        UpdateKillUI();

        if (killCount >= 30) // só ganha depois de matar 30 inimigos
        {
            canWin = true;
        }
    }

    void UpdateKillUI()
    {
        if (killCountText != null)
        {
            killCountText.text = "Kill Count: " + killCount;
        }
    }
}
