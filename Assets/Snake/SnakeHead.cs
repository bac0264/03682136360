using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    public AbstractFactory factory;
    public Snake snake;
    public GameObject deadEffect;
    public GameObject popup;
    public List<Color> listColor;
    int score = 0;
    int star;
    private void Start()
    {
        if (ScoreManager.instance != null)
            star = ScoreManager.instance.getStar();
    }
    public void OnTriggerEnter2D(Collider2D col)
    {
        // Va cham voi cac vat can
        if (col.gameObject.tag.Equals(gameObject.tag) && col.gameObject.layer != 12 && !col.gameObject.layer.Equals(13) && !col.gameObject.layer.Equals(14))
        {
            score++;
            ScoreManager.instance.setScore(score);
            ScoreManager.instance.scoreDisplay();
            ScoreManager.instance.setHighScore(score);
            Destroy(Instantiate(deadEffect, col.transform.position, Quaternion.identity), 3);
            Destroy(col.gameObject);
        }
        // Change color
        else if (col.gameObject.layer.Equals(12))
        {
            score += 2;
            ScoreManager.instance.setScore(score);
            ScoreManager.instance.scoreDisplay();
            ScoreManager.instance.setHighScore(score);
            // int index = factory.getIndexLayerOfObject()
            Destroy(Instantiate(deadEffect, col.transform.position, Quaternion.identity), 3);
            Destroy(col.gameObject);
            int index = factory.getIndexTagOfObject(col.gameObject.tag);
            snake.indexToTransform = index;
            //snake.Change(index,snake.GetComponent<Snake>().circle);
        }
        // Star
        else if (col.gameObject.layer.Equals(13))
        {
            if (ScoreManager.instance != null)
            {
                star++;
                ScoreManager.instance.setStar(star);
                ScoreManager.instance.starDisplay();
                Destroy(Instantiate(deadEffect, col.transform.position, Quaternion.identity), 3);
                Destroy(col.gameObject);
            }
        }
        // Container cua cac barrier
        else if (col.gameObject.layer.Equals(14))
        {

        }
        else
        {
            Time.timeScale = 0;
            Camera.main.GetComponent<MainGameManager>().enabled = false;
            factory.gameObject.SetActive(false);
            snake.enabled = false;
            Instantiate(popup);
        }
    }


}
