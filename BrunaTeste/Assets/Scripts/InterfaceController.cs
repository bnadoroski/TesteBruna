using UnityEngine;
using TMPro;

public class InterfaceController : MonoBehaviour
{
    [SerializeField]
    TMP_Text scoreText;
    [SerializeField]
    TMP_Text timerText;
    [SerializeField]
    GamePlayManager gamePlayManager;
    [SerializeField]
    float timerDefault;

    static int score = 0;
    float timer = 10;

    private void Awake()
    {
        timer = (GameManager.instance?.gameSessionTime ?? timerDefault) * 60;
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            DisplayTime(timer);
        }
    }

    #region Score
    public void AddScore()
    {
        score++;
        scoreText.text = string.Format("Score: {0}", score);
    }

    public void ResetScore() =>
        score = 0;

    public int GetScore() => score;
    #endregion

    #region Timer
    private void DisplayTime(float time)
    {
        if (time < 0)
        {
            time = 0;
            gamePlayManager.ChangeEndMessage("VICTORY");
            gamePlayManager.GameOver();
            Time.timeScale = 0f;
        }

        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StopTimer() =>
        timer = 0;
    #endregion
}
