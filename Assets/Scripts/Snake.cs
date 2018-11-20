using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {

    public GameObject[] listSnakeHead;
    public Material[] listColor;
    public enum color
    {
        Red,
        Blue,
        Green,
        Yellow
    }
    public string tag;
    public color type;
    public static float time;
    void Start () {
        tag = type.ToString();
        gameObject.tag = tag;
	}
	
	// Update is called once per frame
	void Update () {
        Time.timeScale = time;
	}
    //public void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == tag)
    //    {
    //        Debug.Log("run1");

    //        Destroy(collision.gameObject);
    //    }
    //    else
    //    {
    //        Debug.Log("run2");

    //        Time.timeScale = 0;
    //    }
    //}
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals(tag))
        {
            Debug.Log("run3");
            Destroy(col.gameObject);
        }
        else
        {
            Debug.Log("run4");
            time = 0;
        }
    }
}
