using TMPro;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;
    

    [SerializeField] TextMeshProUGUI livesText;

    [SerializeField] TextMeshProUGUI scoreText;


    void Awake()
    {
        int numberOfGameSessions = FindObjectsByType<GameSession>(FindObjectsSortMode.None).Length; // This line counts the number of GameSession objects in the scene.
        if (numberOfGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    
    void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }
    
    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            ReduceLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    void ReduceLife()
    {
        playerLives--;

        FindFirstObjectByType<LevelManager>().ReloadCurrentScene();
        livesText.text = playerLives.ToString();
    }

    void ResetGameSession()
    {
        FindFirstObjectByType<LevelManager>().LoadStartScene();
        resetScore();
        Destroy(gameObject);
        FindFirstObjectByType<ScenePersist>().ResetScenePersist();
    }

    public void increaseScore(int value)
    {
        score += value;
        scoreText.text = score.ToString();
    }

    void resetScore()
    {
        score = 0;
        scoreText.text = score.ToString();
    }
}
