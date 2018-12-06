using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuyingPanel : MonoBehaviour {
    public int curID;
    public GameObject prefabTextEffect;
    public void isYes()
    {
       // gameObject.transform.GetChild(1).GetChild(2).GetComponent<Animator>().Play("ScaleButton");
       // yield return new WaitForSeconds(0.2f);
        int i = curID - 1;
        int requestStar = int.Parse(ShopManager.instance.snakeHeadList[i].cost);
        if (ShopManager.instance.requestStar(requestStar))
        {
            ShopManager.instance.snakeHeadList[i].bought = true;
            PlayerPrefs.SetInt("currentID", ShopManager.instance.snakeHeadList[i].id);
           // ShopManager.instance.reduceStar(requestStar);
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
        Destroy(gameObject);
    }

    public void isNo()
    {
        StartCoroutine(timetoFadeOut());
    }
    IEnumerator timetoFadeOut()
    {
        gameObject.GetComponent<Animator>().Play("out");
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
