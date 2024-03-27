using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text scoreDisplay;
    public Text livesDisplay;
    public Text worldDisplay;
    public Text timer;
    public Text coinCount;


    // Update is called once per frame
    void Update()
    {
        scoreDisplay.text = "mario \n" + LevelManager.Instance.score.ToString("D6");
        worldDisplay.text = $"world \n {LevelManager.Instance.world} - {LevelManager.Instance.level}";
        timer.text = $"Time\n {LevelManager.Instance.time}";
        coinCount.text = "Coins: " + LevelManager.Instance.coinCount.ToString("D2");
        livesDisplay.text = "Lives: " + LevelManager.Instance.lives.ToString("D2");
    }
}
