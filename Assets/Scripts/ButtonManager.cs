using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	public void nextMainGame()
    {
        if (LevelChanger.instance != null)
        {
            LevelChanger.instance.FadeInfc("maingame");
        }
    }
    public void nextShop()
    {
        if (LevelChanger.instance != null)
        {
            LevelChanger.instance.FadeInfc("shop");
        }
    }
    public void backMain()
    {
        Time.timeScale = 1;
        if (LevelChanger.instance != null)
        {
            LevelChanger.instance.FadeInfc("Menu");
        }
    }
    public void RateClick()
    {
        Application.OpenURL("market://details?id=com.Zergitas.ColorSnake2");
    }
}
