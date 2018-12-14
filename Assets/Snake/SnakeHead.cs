using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class SnakeHead : MonoBehaviour
{
    public AbstractFactory factory;
    public Snake snake;
    public List<GameObject> deadEffect;
    public List<string> listTag;
    public List<GameObject> deadCCEffect;
    public GameObject starExp;
    public GameObject popup;
    int score = 0;
    int star;
    bool checkSpecialObject = true;
    public GameObject textEffect;
    private void Start()
    {
        if (ScoreManager.instance != null)
            star = ScoreManager.instance.getStar();
    }
    int getIndexTag(string tag)
    {
        for (int i = 0; i < listTag.Count; i++)
        {
            if (tag.Equals(listTag[i])) return i;
        }
        return -1;
    }
    IEnumerator process(Collider2D col)
    {
        int _index = getIndexTag(col.tag);
        if (col.gameObject.tag.Equals(gameObject.tag) && col.gameObject.layer != 12 && !col.gameObject.layer.Equals(13) && !col.gameObject.layer.Equals(14))
        {
            col.GetComponent<Collider2D>().enabled = false;
            int addScore = 1;
            score += addScore ;
            textEffect.transform.GetChild(0).GetComponent<Text>().text =" + "+ addScore.ToString();
            Destroy(Instantiate(textEffect, col.transform.position + new Vector3(0,1f,0),Quaternion.identity), 2f);
            ScoreManager.instance.setScore(score);
            ScoreManager.instance.scoreDisplay();
            ScoreManager.instance.setHighScore(score);
            Destroy(Instantiate(deadEffect[_index], col.transform.position, Quaternion.identity), 3);
            Tween fadeout = col.transform.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 0), 0.3f);
            col.transform.DOScale(0, 0.5f);
            yield return fadeout.WaitForCompletion();
            Destroy(col.gameObject);
        }
        // Change color
        else if (col.gameObject.layer.Equals(12))
        {
            col.GetComponent<Collider2D>().enabled = false;
            int addScore = 2;
            score += addScore;
            textEffect.transform.GetChild(0).GetComponent<Text>().text = " + " + addScore.ToString();
            Destroy(Instantiate(textEffect, col.transform.position + new Vector3(0, 1, 0), Quaternion.identity), 2f);
            ScoreManager.instance.setScore(score);
            ScoreManager.instance.scoreDisplay();
            ScoreManager.instance.setHighScore(score);
            //int index = factory.getIndexLayerOfObject()
            int index = factory.getIndexTagOfObject(col.gameObject.tag);
            snake.indexToTransform = index;
            Destroy(Instantiate(deadCCEffect[_index], col.transform.position , Quaternion.identity), 3);
            //effect.GetComponent<ParticleSystem>().Play();
            Tween fadeout = col.transform.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 0), 0.3f);
            col.transform.DOScale(0, 0.5f);
            yield return fadeout.WaitForCompletion();
            Destroy(col.gameObject);
            //snake.Change(index,snake.GetComponent<Snake>().circle);
        }
        // Star
        else if (col.gameObject.layer.Equals(13))
        {
            if (ScoreManager.instance != null)
            {
                col.GetComponent<Collider2D>().enabled = false;
                star++;
                int addScore = 1;
                textEffect.transform.GetChild(0).GetComponent<Text>().text = " + " + addScore.ToString();
                Destroy(Instantiate(textEffect, col.transform.position + new Vector3(0, 1, 0), Quaternion.identity), 2f);
                ScoreManager.instance.setStar(star);
                ScoreManager.instance.starDisplay();
                Destroy(Instantiate(starExp, col.transform.position, Quaternion.identity), 3);
                Tween fadeout = col.transform.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 0), 0.3f);
                col.transform.DOScale(0, 0.5f);
                yield return fadeout.WaitForCompletion();
                Destroy(col.gameObject);
            }
        }
        // Container cua cac barrier
        else if (col.gameObject.layer.Equals(14))
        {

        }
        else if (col.gameObject.tag.Equals("Special"))
        {
            snake.checkSpecial = true;
            col.GetComponent<Collider2D>().enabled = false;
            int pos = factory.getIndexTagOfObject(gameObject.tag);
            string tagName = gameObject.tag;
            //int index = factory.getIndexLayerOfObject()
            //snake.indexToTransform = pos + 4;
            gameObject.tag = "Special";
            Destroy(Instantiate(starExp, col.transform.position, Quaternion.identity), 3);
            Tween fadeout = col.transform.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 0), 0.3f);
            yield return fadeout.WaitForCompletion();
            Destroy(col.gameObject);
            yield return new WaitForSeconds(4f);
            if (checkSpecialObject)
            {
                snake.indexToTransform = pos;
                gameObject.tag = tagName;
                snake.checkSpecial = false;
            }
            else
            {
                checkSpecialObject = true;
            }
        }
        else
        {
            Time.timeScale = 1;
            Camera.main.GetComponent<MainGameManager>().enabled = false;
            factory.gameObject.SetActive(false);
            snake.enabled = false;
            if (col.gameObject.GetComponentInParent<Animator>() != null)
                col.gameObject.GetComponentInParent<Animator>().enabled = false;
            snake.transform.GetChild(0).GetComponent<Collider2D>().enabled = false;
            Instantiate(popup);
        }
    }
    IEnumerator process_2(Collider2D col)
    {
        int _index = getIndexTag(col.tag);
        if (col.gameObject.layer != 12 && !col.gameObject.layer.Equals(13) && !col.gameObject.layer.Equals(14) && !col.gameObject.tag.Equals("Special"))
        {
            col.GetComponent<Collider2D>().enabled = false;
            int addScore = 1;
            score += addScore;
            textEffect.transform.GetChild(0).GetComponent<Text>().text = " + " + addScore.ToString();
            Destroy(Instantiate(textEffect, col.transform.position + new Vector3(0, 1, 0) , Quaternion.identity), 2f);
            ScoreManager.instance.setScore(score);
            ScoreManager.instance.scoreDisplay();
            ScoreManager.instance.setHighScore(score);
            Destroy(Instantiate(deadEffect[_index], col.transform.position, Quaternion.identity), 3);
            Tween fadeout = col.transform.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 0), 0.3f);
            col.transform.DOScale(0, 0.5f);
            yield return fadeout.WaitForCompletion();
            Destroy(col.gameObject);
        }
        // Change color
        else if (col.gameObject.layer.Equals(12))
        {
            col.GetComponent<Collider2D>().enabled = false;
            int addScore = 2;
            score += addScore;
            textEffect.transform.GetChild(0).GetComponent<Text>().text = " + " + addScore.ToString();
            Destroy(Instantiate(textEffect, col.transform.position + new Vector3(0, 1, 0), Quaternion.identity), 2f);
            ScoreManager.instance.setScore(score);
            ScoreManager.instance.scoreDisplay();
            ScoreManager.instance.setHighScore(score);
            int index = factory.getIndexTagOfObject(col.gameObject.tag);
            snake.indexToTransform = index;
            snake.checkSpecial = false;
            checkSpecialObject = false;
            Destroy(Instantiate(deadCCEffect[_index], col.transform.position, Quaternion.identity), 3);
            Tween fadeout = col.transform.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 0), 0.3f);
            col.transform.DOScale(0, 0.5f);
            yield return fadeout.WaitForCompletion();
            Destroy(col.gameObject);

        }
        // Star
        else if (col.gameObject.layer.Equals(13))
        {
            if (ScoreManager.instance != null)
            {
                col.GetComponent<Collider2D>().enabled = false;
                star++;
                int addScore = 1;
                textEffect.transform.GetChild(0).GetComponent<Text>().text = " + " + addScore.ToString();
                Destroy(Instantiate(textEffect , col.transform.position + new Vector3(0, 1, 0), Quaternion.identity), 2f);
                ScoreManager.instance.setStar(star);
                ScoreManager.instance.starDisplay();
                Destroy(Instantiate(starExp, col.transform.position, Quaternion.identity), 3);
                Tween fadeout = col.transform.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 0), 0.3f);
                col.transform.DOScale(0, 0.5f);
                yield return fadeout.WaitForCompletion();
                Destroy(col.gameObject);
            }
        }
        else if (col.gameObject.tag.Equals("Special"))
        {
            col.GetComponent<Collider2D>().enabled = false;
            //int pos = factory.getIndexTagOfObject(gameObject.tag);
            //string tagName = gameObject.tag;
            ////int index = factory.getIndexLayerOfObject()
            //snake.indexToTransform = pos + 4;
            //gameObject.tag = "Special";
            Destroy(Instantiate(starExp, col.transform.position, Quaternion.identity), 3);
            Tween fadeout = col.transform.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 0), 0.3f);
            yield return fadeout.WaitForCompletion();
            Destroy(col.gameObject);
            //yield return new WaitForSeconds(4f);
            //if (checkSpecialObject)
            //{
            //    snake.indexToTransform = pos;
            //    gameObject.tag = tagName;
            //}
            //else
            //{
            //    checkSpecialObject = true;
            //}
        }
    }
    public void OnTriggerEnter2D(Collider2D col)
    {
        // Va cham voi cac vat can
        if (gameObject.tag.Equals("Special"))
        {
            StartCoroutine(process_2(col));
        }
        else
        {
            StartCoroutine(process(col));
        }
    }


}
