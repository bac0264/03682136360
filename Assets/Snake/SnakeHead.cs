using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour {
    public AbstractFactory factory;
    public Snake snake;
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals(gameObject.tag) && col.gameObject.layer != 12 )
        {
            Debug.Log("run3");
            Destroy(col.gameObject);
        }
        else if (col.gameObject.layer.Equals(12))
        {
            // int index = factory.getIndexLayerOfObject()
            Destroy(col.gameObject);
            int index = factory.getIndexTagOfObject(col.gameObject.tag);
            snake.Change(index);
        }
        else {
            Time.timeScale = 0;
            Debug.Log("run4");
        }
    }

}
