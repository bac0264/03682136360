using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScaler : MonoBehaviour {
    void Start () {
        if (LevelChanger.instance != null)
        {
            LevelChanger.instance.FadeOutfc();
        }
    }
	
}
