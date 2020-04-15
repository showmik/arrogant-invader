using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static bool gameOver = false;
    public int playerLives = 3;

    public static int healthIndex;
    private Player player;
    public Transform playerSpawnPoint;
    public GameObject playerPrefab;
    public GameObject gameOverPanel;
    public GameObject gameWonPanel;
    public GameObject[] healthUI;

    void Start()
    {
        Time.timeScale = 1f;
        healthIndex = healthUI.Length - 1;
        SpawnPlayer();
    }

    void Update()
    {
        
    }

    public void GameOver()
    {
        StartCoroutine(player.Die()) ;
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GameWon()
    {
        FindObjectOfType<AudioManager>().Play("GameWon");
        gameWonPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void SpawnPlayer()
    {
        GameObject playerInstance = Instantiate(playerPrefab, playerSpawnPoint);
        player = playerInstance.GetComponent<Player>();
    }

    public void Replay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
