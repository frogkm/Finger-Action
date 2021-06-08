using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class ScoreKeeper : MonoBehaviour
{
    public Text scoreText;
    public Text highscoreText;
    private int highScore;

    void Start() {
        if (!PlayerPrefs.HasKey("highscore")) {
            PlayerPrefs.SetInt("highscore", 0);
            PlayerPrefs.Save();
        }
        scoreText.text = "You Got: " + PlayerController.score.ToString();
        highScore = loadHighScore();
        highscoreText.text = "Highscore: " + highScore.ToString();
        if (PlayerController.score > highScore) {
            writeHighScore();
            highScore = PlayerController.score;
            highscoreText.text = "New Highscore! " + highScore.ToString();
        }
    }

    private int loadHighScore() {
        return PlayerPrefs.GetInt("highscore");
    }

    private void writeHighScore() {
        PlayerPrefs.SetInt("highscore", PlayerController.score);
        PlayerPrefs.Save();
    }




}
