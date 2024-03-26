using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance {get; private set;}

    public int world { get;private set;}
    public int level { get; private set;}
    public int lives { get; private set; }

    private int score;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        { 
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        { 
            Instance = null;
        }
    }

    private void Start()
    {
        NewGame();    
    }

    private void NewGame()
    {
        lives = 3;
        score = 0;

        LoadWorld(1,1);
    }

    private void LoadWorld(int world, int level)
    { 
        this.world = world;
        this.level = level;

        SceneManager.LoadScene($"{world}-{level}");
    }

    public void ResetScene(float delay)
    {
        Invoke(nameof(ResetScene), delay);
    }

    public void ResetScene()
    {
        lives--;
        if (lives > 0)
        {
            LoadWorld(world, level);
        }
        else 
        {
            GameOver();
        }
    }

    private void GameOver() 
    {
        NewGame();
    }

    public void AddLife()
    { 
        lives++; 
    }

    public void AddCoin() 
    {
        score += 100;
        if (score % 2000 == 0) 
        {
            AddLife();
        }
    }
}
