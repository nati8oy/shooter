using UnityEngine;
using MoreMountains.Tools;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab; // The enemy prefab to spawn
    public int numberOfEnemies = 5; // Number of enemies to spawn
    public float spawnRadius = 10f; // Maximum distance from the center to spawn enemies
    public GameObject gameOverUI; // Reference to the game over UI canvas

    public PlayerHealth playerHealth; // Reference to the player's health component
    public GameObject player;

    private bool isGameOver = false;
    [SerializeField] private bool SpawnRandoms;

    void Start()
    {
        // Spawn all enemies at once

        if (SpawnRandoms)
        {
            SpawnEnemy();
        }

    }

    void Update()
    {
        // Check if the player's health has reached zero
        if (!isGameOver && playerHealth.currentHealth <= 0)
        {
            // Trigger game over
            GameOver();
        }
    }

    void SpawnEnemy()
    {
        // Spawn the specified number of enemies
        for (int i = 0; i < numberOfEnemies; i++)
        {
            // Generate a random position within the spawn radius
            Vector3 spawnPosition = GetRandomSpawnPosition();

            // Instantiate the enemy at the random position
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        // Generate a random position within the spawn radius
        Vector3 spawnPosition = Random.insideUnitCircle * spawnRadius;
        spawnPosition.z = 0f; // Set z-coordinate to 0 for 2D
        return spawnPosition;
    }

    void GameOver()
    {
        isGameOver = true;


        player.SetActive(false);

        // Pause the game
       // Time.timeScale = 0f;

        // Display game over UI
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
    }

    public void RestartGame()
    {
        // Reload the current scene to restart the game
       // Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

}
