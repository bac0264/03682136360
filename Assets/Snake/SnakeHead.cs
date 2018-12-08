using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
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
    private void Start()
    {
        if (ScoreManager.instance != null)
            star = ScoreManager.instance.getStar();
    }
    int getIndexTag(string tag)
    {
        for(int i = 0; i < listTag.Count; i++)
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
            score++;
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
            score += 2;
            ScoreManager.instance.setScore(score);
            ScoreManager.instance.scoreDisplay();
            ScoreManager.instance.setHighScore(score);
            //int index = factory.getIndexLayerOfObject()
            Destroy(Instantiate(deadCCEffect[_index], col.transform.position, Quaternion.identity), 3);
            //effect.GetComponent<ParticleSystem>().Play();
            Tween fadeout = col.transform.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 0), 0.3f);
            col.transform.DOScale(0, 0.5f);
            yield return fadeout.WaitForCompletion();
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
                col.GetComponent<Collider2D>().enabled = false;
                star++;
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
        else
        {
            Time.timeScale = 0;
            Camera.main.GetComponent<MainGameManager>().enabled = false;
            factory.gameObject.SetActive(false);
            snake.enabled = false;
            Instantiate(popup);
        }
    }
    public void OnTriggerEnter2D(Collider2D col)
    {
        // Va cham voi cac vat can
        StartCoroutine(process(col));
    }


}
