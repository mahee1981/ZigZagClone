using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool gameStarted;
    private int _score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    private void Awake()
    {
        highScoreText.text = $"Best: {GetHighScore()}";
    }

    public void StartGame()
    {
        gameStarted = true;
    }

    public void EndGame()
    {
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartGame();
        }
    }

    public void IncreaseScore()
    {
        _score++;
        scoreText.text = _score.ToString();

        if (_score > GetHighScore())
        {
            PlayerPrefs.SetInt("HighScore", _score);
            highScoreText.text = _score.ToString();
        }
    }

    public int GetHighScore()
    {
        var i = PlayerPrefs.GetInt("HighScore");
        return i;
    }
    
}
