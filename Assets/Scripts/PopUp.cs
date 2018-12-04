using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PopUp : MonoBehaviour {
    public Text highText;
    public Text currentText;
    private void Start()
    { 
        gameObject.GetComponent<Animator>().Play("show");
        if(ScoreManager.instance != null)
        {
            ScoreManager.instance.highScoreDisPlay(highText);
            ScoreManager.instance.scoreDisplay(currentText);
        }
    }
    public void watchVideo()
    {

    }
    public void PlayAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("maingame");
    }
    IEnumerator TimetoPlayAgain()
    {
        gameObject.GetComponent<Animator>().Play("fadein");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("maingame");
    }
}
