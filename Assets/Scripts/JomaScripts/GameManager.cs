using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private DatabaseHandler databaseHandler;
    [SerializeField] private StudentSO studentSO;
    [SerializeField] private Authentification authentification;

    private int currentScore = 0;
    private int highScore = 0;

    private int lastDisplayedScore = -1;
    private int lastDisplayedHighScore = -1;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateScoreDisplay(); // Mostrar los puntajes al inicio
    }

    private void Update()
    {
        // Mostrar puntajes en tiempo real si cambian
        UpdateScoreDisplay();
    }

    public void UpdateScore(int score)
    {
        currentScore = score;

        // Si superamos el high score, lo actualizamos en tiempo real
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }

    public void AddScore(int points)
    {
        currentScore += points;

        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }

    private void UpdateScoreDisplay()
    {
        if (lastDisplayedScore != currentScore)
        {
            scoreText.text = $"Score: {currentScore}";
            lastDisplayedScore = currentScore;
        }

        if (lastDisplayedHighScore != highScore)
        {
            highScoreText.text = $"High Score: {highScore}";
            lastDisplayedHighScore = highScore;
        }
    }

    public void SaveScoreLocal(string playerName, int score)
    {
        int[] scores = new int[5];
        string[] names = new string[5];

        for (int i = 0; i < 5; i++)
        {
            scores[i] = PlayerPrefs.GetInt($"score_{i}", 0);
            names[i] = PlayerPrefs.GetString($"name_{i}", "---");
        }

        for (int i = 0; i < 5; i++)
        {
            if (score > scores[i])
            {
                for (int j = 4; j > i; j--)
                {
                    scores[j] = scores[j - 1];
                    names[j] = names[j - 1];
                }
                scores[i] = score;
                names[i] = playerName;
                break;
            }
        }

        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetInt($"score_{i}", scores[i]);
            PlayerPrefs.SetString($"name_{i}", names[i]);
        }
        PlayerPrefs.Save();
    }

    public void GameOver(int finalScore)
    {
        finalScoreText.text = $"Final Score: {currentScore}";
        gameOverPanel.SetActive(true);

        scoreText.gameObject.SetActive(false);
        highScoreText.gameObject.SetActive(false);

        string playerName = studentSO.PlayerName;
        SaveScoreLocal(playerName, currentScore);

        if (databaseHandler != null)
        {
            databaseHandler.SaveScoretoFirebase(playerName, currentScore);
            databaseHandler.UpdateStudentScoreInFirebase(studentSO.Id, currentScore);
        }

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
