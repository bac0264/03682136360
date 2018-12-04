using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int score = 0;
    public Text scoreText;
    public int star = 0;
    public Text starText;
    public const string highScore = "HighScore";
    public const string starScore = "StarScore";
    // Use this for initialization
    private void Awake()
    {
        if (instance == null) instance = this;
        star = PlayerPrefs.GetInt(starScore);
        starDisplay();
    }
    public void scoreDisplay()
    {
        scoreText.text = getScore().ToString();
    }
    public void scoreDisplay(Text scoreText)
    {
        scoreText.text = getScore().ToString();
    }
    public void starDisplay()
    {
        starText.text = getStar().ToString();
    }
    public void highScoreDisPlay(Text highText)
    {
        highText.text = PlayerPrefs.GetInt(highScore).ToString();
    }
    public void setStar(int _star)
    {
        PlayerPrefs.SetInt(starScore, _star);
    }
    public int getStar()
    {
        return PlayerPrefs.GetInt(starScore);
    }
    public void setScore(int _score)
    {
        score = _score;
    }
    public int getScore()
    {
        return score;
    }
    public void setHighScore(int _score)
    {
        if (_score > PlayerPrefs.GetInt(highScore))
        {
            PlayerPrefs.SetInt(highScore, _score);
        }
    }
    public int getHighScore()
    {
        return PlayerPrefs.GetInt(highScore);
    }
}
