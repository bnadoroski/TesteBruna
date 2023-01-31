using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pathfinding;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject pathfinding;
    [SerializeField]
    GameObject gameOverScreen;
    [SerializeField]
    GameObject optionsScreen;
    [SerializeField]
    GameObject mainMenuScreen;
    [SerializeField] 
    TMP_Text finalScoreText;

    public enum EnemyType
    {
        Shooter = 1,
        Chaser = 2
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void Options()
    {
        optionsScreen.SetActive(true);
        mainMenuScreen.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver(int score)
    {
        Destroy(pathfinding.GetComponent<ProceduralGridMover>());
        gameOverScreen.SetActive(true);
        finalScoreText.text = string.Format("Score: {0}", score);

    }
}
