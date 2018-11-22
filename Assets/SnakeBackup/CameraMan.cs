using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMan : MonoBehaviour {
    // Use this for initialization
    float widthScreen = 6;
    void Start () {
        Camera.main.orthographicSize = widthScreen / Screen.width * Screen.height / 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }
}
