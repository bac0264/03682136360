using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class PopUp : MonoBehaviour {
    public Text highText;
    public Text currentText;
    public GameObject panel;
    private void Start()
    {
        gameObject.GetComponent<Animator>().Play("show");
        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.highScoreDisPlay(highText);
            ScoreManager.instance.scoreDisplay(currentText);
        }
        if (panel != null)
        {
            panel.SetActive(true);
            panel.GetComponent<Image>().DOColor(new Color(0, 0, 0, 225f / 255), 0.5f);
        }
    }

    public void PlayAgain()
    {
        Time.timeScale = 1;
        //StartCoroutine(TimetoPlayAgain());
        SceneManager.LoadScene("maingame");
    }
    IEnumerator TimetoPlayAgain()
    {
        gameObject.GetComponent<Animator>().Play("fadein");
        Tween _fadeout = panel.GetComponent<Image>().DOColor(new Color(0, 0, 0, 0), 0.5f);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("maingame");
    }
    public void WatchVideo()
    {
        panel.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        PlayerPrefs.SetInt("currentScore", ScoreManager.instance.score);
        PlayerPrefs.SetInt("checkWatchVideo", 1);
        AdManager.Ins.ShowVideo();
        SceneManager.LoadScene("maingame");
    }
    public void _backMenu()
    {
        if(LevelChanger.instance != null)
            LevelChanger.instance.FadeInfc("Menu");
    }
}
