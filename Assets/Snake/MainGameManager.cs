using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum GameStatus
{
    READY,
    PLAYING,
    GAMEOVER
}
public class MainGameManager : MonoBehaviour {
    public GameObject factory;
    float lastXmouse = 1000,widthScreen=6;
    public static GameStatus gameStatus = GameStatus.READY;
    private void Start()
    {
        Camera.main.orthographicSize = widthScreen / Screen.width * Screen.height/2;
    }
    // Update is called once per frame
    void FixedUpdate () {
        //Move camera
        //if (gameStatus == GameStatus.PLAYING)
        //{
        //    factory.SetActive(true);
        //    Camera.main.transform.position = new Vector3(0, Camera.main.transform.position.y + Time.deltaTime * 3.2f, -10);
        //}
        Camera.main.transform.position = new Vector3(0, Camera.main.transform.position.y + Time.deltaTime * 3.2f, -10);
        //Target Moving
        if (lastXmouse != 1000)
        {
            transform.GetChild(0).localPosition += new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x-lastXmouse, 0, 0);
            lastXmouse = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            if(transform.GetChild(0).localPosition.x< -widthScreen/2+0.1f) transform.GetChild(0).localPosition = new Vector3(-widthScreen/2+0.1f, transform.GetChild(0).localPosition.y, transform.GetChild(0).localPosition.z);
            if (transform.GetChild(0).localPosition.x > widthScreen/2-0.1f) transform.GetChild(0).localPosition = new Vector3(widthScreen/2-0.1f, transform.GetChild(0).localPosition.y, transform.GetChild(0).localPosition.z);
        }
        if (Input.GetMouseButtonDown(0))
        {
            lastXmouse = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
           // if (gameStatus == GameStatus.READY) gameStatus = GameStatus.PLAYING;
        }
        if (Input.GetMouseButtonUp(0))
        {
            lastXmouse = 1000;
        }
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                lastXmouse = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
              //  if (gameStatus == GameStatus.READY) gameStatus = GameStatus.PLAYING;
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {

            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                lastXmouse = 1000;
            }
        }

    }
    public void Retry()
    {
        gameStatus = GameStatus.READY;
        SceneManager.LoadScene("maingame");
        Time.timeScale = 1;
    }
}
