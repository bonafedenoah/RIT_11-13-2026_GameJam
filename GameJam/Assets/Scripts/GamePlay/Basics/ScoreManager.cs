using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private int playerScore = 0;
    [SerializeField] GameObject player;

    [Header("UI Text References")]
    [SerializeField] private TMP_Text playerScoreText;
    [SerializeField] private TMP_Text playerHealthText;

    [Header("Game Over UI")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text winnerText;

    [Header("Victory Sound")]
    AudioManager audioManager;

    private bool isGameOver = false;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

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
        UpdateScoreUI();

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (isGameOver && Input.GetKeyDown(KeyCode.Space))
        {
            RestartGame();
        }
    }

    public void AddPlayer1Score()
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
            playerScoreText.text = "Score: "+playerScore.ToString();
            playerHealthText.text = "Health: "+player.GetComponent<PlayerController>().Health.ToString();
        }
    }

    private void CheckLoseCondition()
    {
        if(player.GetComponent<PlayerController>().Health <= 0)
        {
            TriggerGameOver("You Lose!");
        }   
    }

    private void TriggerGameOver(string winnerMessage)
    {
        isGameOver = true;

        audioManager.PlaySFX(audioManager.victory);

        if (winnerText != null)
        {
            winnerText.text = winnerMessage;
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
}
