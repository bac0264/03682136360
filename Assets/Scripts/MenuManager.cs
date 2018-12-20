using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuManager : MonoBehaviour {
    public Button btnAudio;
    public Sprite audioOn, audioOff;
    void Start()
    {
        Application.targetFrameRate = 60;
        if (PlayerPrefs.GetInt("audio", 1) == 1)
        {
            btnAudio.GetComponent<Image>().sprite = audioOn;
            AudioListener.volume = 1;
        }
        else
        {
            btnAudio.GetComponent<Image>().sprite = audioOff;
            AudioListener.volume = 0;
        }

    }
    // Use this for initialization
    void Awake () {
        _IsGameStartedForTheFirstTime();
	}

    public void AudioClick()
    {
        PlayerPrefs.SetInt("audio", -PlayerPrefs.GetInt("audio", 1));
        if (PlayerPrefs.GetInt("audio", 1) == 1)
        {
            btnAudio.GetComponent<Image>().sprite = audioOn;
            AudioListener.volume = 1;
        }
        else
        {
            btnAudio.GetComponent<Image>().sprite = audioOff;
            AudioListener.volume = 0;
        }
    }
    void _IsGameStartedForTheFirstTime()
    {

        if (!PlayerPrefs.HasKey("IsGameStartedForTheFirstTime"))
        {
            PlayerPrefs.SetInt("currentID", 1);
            PlayerPrefs.SetInt("IsGameStartedForTheFirstTime", 0);
        }

    }
    public void RateClick()
    {
        Application.OpenURL("market://details?id=com.zergitas.happyglass2");
    }
}
