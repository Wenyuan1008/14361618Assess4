using UnityEngine;
using TMPro;

public class HighScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI highScoreTimeText;

    void Start()
    {
        // Load high scores
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        string highScoreTime = PlayerPrefs.GetString("HighScoreTime", "00:00:00");

        highScoreText.text = "High Score: " + highScore;
        highScoreTimeText.text = "Time: " + highScoreTime;
    }
}
