using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class BuyingPanel : MonoBehaviour
{
    public int curID;
    public GameObject prefabTextEffect;
    public GameObject panel;
    private void Start()
    {
        if (panel != null)
        {
            panel.SetActive(true);
            panel.GetComponent<Image>().DOColor(new Color(0, 0, 0, 225f / 255), 0.5f);
        }
    }
    public void isYes()
    {
        // gameObject.transform.GetChild(1).GetChild(2).GetComponent<Animator>().Play("ScaleButton");
        // yield return new WaitForSeconds(0.2f);
        StartCoroutine(timetoFade());
    }
    IEnumerator timetoFade()
    {
        int i = curID - 1;
        int requestStar = int.Parse(ShopManager.instance.snakeHeadList[i].cost);
        if (ShopManager.instance.requestStar(requestStar))
        {
            ShopManager.instance.snakeHeadList[i].bought = true;
            PlayerPrefs.SetInt("currentID", ShopManager.instance.snakeHeadList[i].id);
            ShopManager.instance.reduceStar(requestStar);
            ShopManager.instance.boughtList.Add(ShopManager.instance.snakeHeadList[i]);
            ShopManager.instance.currentID = curID;
            ShopManager.instance.UpdateUI();
            prefabTextEffect.transform.GetChild(0).GetComponent<Text>().text = "-" + requestStar;
            Instantiate(prefabTextEffect, ShopManager.instance.snakeHeadObjectList[i].transform);
            SaveLoad.instance.saving();

        }
        else
        {
            // prefabTextEffect.transform.GetChild(0).GetComponent<Text>().text = "Not enough money";
            prefabTextEffect.transform.GetChild(0).GetComponent<Text>().text = "Not enough money";
            Instantiate(prefabTextEffect, ShopManager.instance.snakeHeadObjectList[i].transform);
        }
        gameObject.GetComponent<Animator>().Play("out");
        Tween _fadeout = panel.GetComponent<Image>().DOColor(new Color(0, 0, 0, 0), 0.5f);
        yield return _fadeout.WaitForCompletion();
        Destroy(gameObject);
    }
    public void isNo()
    {
        StartCoroutine(timetoFadeOut());
    }
    IEnumerator timetoFadeOut()
    {
        gameObject.GetComponent<Animator>().Play("out");
        Tween _fadeout = panel.GetComponent<Image>().DOColor(new Color(0, 0, 0, 0), 0.5f);
        yield return _fadeout.WaitForCompletion();
        Destroy(gameObject);
    }
}
