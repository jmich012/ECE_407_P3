using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance {get; private set;}

    public int world { get;private set;}
    public int level { get; private set;}
    public int lives { get; private set; }

    public int score { get; private set; }

    public int time { get; private set; }
    public int coinCount { get; private set; }

    private float timer = 0;
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

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1.0f)
        {
            time--;
            timer = 0.0f; 
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
        coinCount = 0;
        time = 400;

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

    public void AddCoin(int multiplier) 
    {
        score += 100 * multiplier;
        coinCount++;
        if (score % 2000 == 0) 
        {
            AddLife();
        }
    }

    public void KillGoomba()
    {
        score += 100;
    }

    public void KillKoopa()
    {
        score += 200;
    }
}
