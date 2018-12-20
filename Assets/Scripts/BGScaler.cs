using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BGScaler : MonoBehaviour
{
    void Start()
    {
        if (LevelChanger.instance != null)
        {
            LevelChanger.instance.FadeOutfc();
        }
    }
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            if (LevelChanger.instance != null)
            {
                if (!SceneManager.GetActiveScene().name.Equals("Menu"))
                    LevelChanger.instance.FadeInfc("Menu");
                else
                {
                    Application.Quit();
                }
            }
        }
    }

}
