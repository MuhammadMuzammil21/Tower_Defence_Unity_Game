using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;



public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject enemyPrefab;
    public Transform[] spawnPoints;

    public int enemiesReachedBase = 0;
    public int maxEnemiesAllowed = 10;

    public int score = 0;
    public int baseHealth = 10;

    private int totalEnemiesSpawned = 0;
    private int enemiesDestroyed = 0;
    private int enemiesReachedBaseCount = 0;
    private bool allWavesSpawned = false;


    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI baseHealthText;
    public GameObject gameOverPanel;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateUI();
        StartCoroutine(SpawnEnemyWaves());
    }

    public void EnemyDefeated()
    {
        score++;
        enemiesDestroyed++;
        UpdateUI();
        CheckEndCondition();
    }

    public void EnemyReachedBase()
    {
        baseHealth--;
        enemiesReachedBaseCount++;
        UpdateUI();

        if (baseHealth <= 0)
        {
            UIManager.Instance.ShowGameOver();
        }
        else
        {
            CheckEndCondition();
        }
    }

    void UpdateUI()
    {
        if (scoreText) scoreText.text = $"Score: {score}";
        if (baseHealthText) baseHealthText.text = $"Base HP: {baseHealth}";
    }

    void GameOver()
    {
        Debug.Log("Game Over!");
        Time.timeScale = 0f;
        if (gameOverPanel) UIManager.Instance.ShowGameOver();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    private IEnumerator SpawnEnemyWaves()
    {
        int enemiesToSpawn = 2;

        for (int wave = 1; wave <= 5; wave++)
        {
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                Transform randomSpawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Instantiate(enemyPrefab, randomSpawn.position, Quaternion.identity);
                totalEnemiesSpawned++;
                yield return new WaitForSeconds(1f);
            }

            yield return new WaitForSeconds(3f); // delay between waves
            enemiesToSpawn += 1;
        }

        allWavesSpawned = true;
        Debug.Log("✅ All waves spawned!");
        CheckEndCondition();
    }


    private void CheckEndCondition()
    {
        Debug.Log($"Checking End: Destroyed={enemiesDestroyed}, ReachedBase={enemiesReachedBaseCount}, TotalSpawned={totalEnemiesSpawned}, AllWavesSpawned={allWavesSpawned}");

        if (allWavesSpawned && (enemiesDestroyed + enemiesReachedBaseCount >= totalEnemiesSpawned))
        {
            if (baseHealth > 0)
            {
                Debug.Log("WIN TRIGGERED");
                UIManager.Instance.ShowWinScreen();
            }
            else
            {
                Debug.Log("LOSS TRIGGERED");
                UIManager.Instance.ShowGameOver();
            }
        }
    }
}
