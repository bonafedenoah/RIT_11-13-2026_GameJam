using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    private int playerScore;
    private float scoreMultiplier;

    [SerializeField] GameObject player;

    [Header("UI Text References")]
    [SerializeField] private TMP_Text playerScoreText;
    [SerializeField] private TMP_Text playerHealthText;

    [Header("Game Over UI")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text winnerText;

    [Header("Victory Sound")]
    AudioManager audioManager;

    private BuildingSpawner buildingSpawner;
    private SpriteThemeManager spriteThemeManager;

    private bool isGameOver = false;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        buildingSpawner = GameObject.FindGameObjectWithTag("BuildingManager").GetComponent<BuildingSpawner>();
        spriteThemeManager = GameObject.FindGameObjectWithTag("BuildingManager").GetComponent<SpriteThemeManager>();

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        playerScore = 0;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    private void Update()
    {
        UpdatePlayerScore();

        if (isGameOver && Keyboard.current.spaceKey.isPressed)
        {
            RestartGame();
        }
    }

    public void UpdatePlayerScore()
    {
        if (isGameOver)
        {
            return;
        }

        UpdateScoreUI();
        CheckLoseCondition();
    }

    private void UpdateScoreUI()
    {
        if (playerScoreText != null)
        {
            playerScoreText.text = "Score: " + playerScore;
            playerHealthText.text = "Health: "+player.GetComponent<PlayerController>().Health.ToString();
        }
    }

    private void CheckLoseCondition()
    {
        if(player.GetComponent<PlayerController>().Health <= 0)
        {
            TriggerGameOver("You Lose!\n" +
                            "\nYou Scored " + playerScore + " Points!");
        }   
        else if(buildingSpawner.CurrentBuildings<=0)
        {
            TriggerGameOver("You Lose!\n" +
                            "\nYou Scored " + playerScore + " Points!");
        }
    }

    private void TriggerGameOver(string LosingMessage)
    {
        isGameOver = true;

        if (winnerText != null)
        {
            winnerText.text = LosingMessage;
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddPlayerScore(int points)
    {
        GetScoreMultiplier();
        playerScore += (int)(points * scoreMultiplier);
    }

    private void GetScoreMultiplier()
    {
        if (!spriteThemeManager.IsAlternateTheme)
        {
            scoreMultiplier = 1.5f;
        }
        else
        {
            scoreMultiplier = 1.0f;
        }
    }
}
