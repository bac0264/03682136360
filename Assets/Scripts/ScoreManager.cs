using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int score = 0;
    public Text scoreText;
    public int star;
    public Text starText;
    public const string highScore = "HighScore";
    public const string starScore = "StarScore";
    // Use this for initialization
    private void Awake()
    {
        if (instance == null) instance = this;
        _setupScore();
        Debug.Log("SaveLoad: " + SaveLoad.instance);
        if (SaveLoad.instance != null)
            SaveLoad.instance.loadingStar();
        starDisplay();
        Debug.Log("Score:" + score);
        Debug.Log("Star: " + star);
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
        star = _star;
    }
    public int getStar()
    {
        return star;
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
    public void _setupScore()
    {
        if (PlayerPrefs.GetInt("checkWatchVideo") == 1)
        {
            PlayerPrefs.SetInt("checkWatchVideo", 0);
            setScore(PlayerPrefs.GetInt("currentScore"));
            scoreDisplay();
        }
        else PlayerPrefs.SetInt("currentScore", 0);
    }
}
