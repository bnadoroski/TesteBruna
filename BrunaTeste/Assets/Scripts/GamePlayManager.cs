using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Pathfinding;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField]
    GameObject gameOverScreen;
    [SerializeField]
    TMP_Text finalScoreText;
    [SerializeField]
    TMP_Text endMessage;
    [SerializeField]
    public float gameSessionTime;
    [SerializeField]
    GameObject pathfinding;
    [SerializeField]
    InterfaceController uiController;

    public static GamePlayManager instance;

    public enum EnemyType
    {
        Shooter = 1,
        Chaser = 2
    }

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    private void Start()
    {
        gameSessionTime = GameManager.instance?.gameSessionTime ?? 1f;
    }

    public void Timer()
    {

    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        Destroy(pathfinding.GetComponent<ProceduralGridMover>());
        finalScoreText.text = string.Format("SCORE: {0}", uiController.GetScore());
        uiController.StopTimer();
    }

    public void Restart()
    {
        uiController.ResetScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ChangeEndMessage(string message) =>
        endMessage.text = message;

    public void MainMenu() =>
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
}
