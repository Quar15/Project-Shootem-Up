using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI gameOverHighscoreText;
    [SerializeField] private TextMeshProUGUI gameOverScoreText;
    private int _score;
    private int _highscore;

    private void Awake()
    {
        _score = 0;
        UpdateScoreText();
    }

    public void UpdateScoreText()
    {
        scoreText.text = _score.ToString();
    }

    public void AddScore(int scoreToAdd)
    {
        _score += scoreToAdd;
        UpdateScoreText();
    }

    public void SaveScore()
    {
        if(PlayerPrefs.HasKey("Highscore"))
            _highscore = PlayerPrefs.GetInt("Highscore");

        if(_highscore < _score)
        {
            _highscore = _score;
            PlayerPrefs.SetInt("Highscore", _highscore);
        }

        gameOverHighscoreText.text = "Highscore: " + _highscore.ToString();
        gameOverScoreText.text = "Score: " + _score.ToString();
    }
}
