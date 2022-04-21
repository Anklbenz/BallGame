
using UnityEngine;
using UnityEngine.UI;

public class UIGameOverScreen : MonoBehaviour
{
    [SerializeField] private Text _scoreLabel;
    [SerializeField] private Text _highScoreLabel;
    private int Score;

    public void SetScores(int score)
    {
        _scoreLabel.text = score.ToString();
        _highScoreLabel.text = PlayerPrefs.GetInt("HighScore", 0).ToString();

        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }
}
