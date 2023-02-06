using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    [SerializeField]
    TMP_Text gameSessionText;
    [SerializeField]
    TMP_Text SpawnEnemyText;
    [SerializeField]
    Slider gameSessionSlider;
    [SerializeField]
    Slider SpawnEnemySlider;

    private void Awake()
    {
        SetGameSessionTime(GameManager.instance.gameSessionTime);
        SetSpawnEnemyTime(GameManager.instance.spawnEnemyTime);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SetGameSessionTime(float gameTime)
    {
        gameTime = gameTime == 0 ? 2f : gameTime;
        gameSessionText.text = string.Format("{0} minute{1}", gameTime, gameTime > 1 ? "s" : "");
        gameSessionSlider.value = gameTime;
        GameManager.instance.gameSessionTime = gameTime;
    }

    public void SetSpawnEnemyTime(float spawnTime)
    {
        spawnTime = spawnTime == 0 ? 10f : spawnTime;
        SpawnEnemyText.text = string.Format("{0} second{1}", spawnTime, spawnTime > 1 ? "s" : "");
        SpawnEnemySlider.value = spawnTime;
        GameManager.instance.spawnEnemyTime = spawnTime;
    }
}
