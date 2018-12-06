using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyButton : MonoBehaviour {

    public static BuyButton instance;
    public int snakeID;
    public GameObject Panel;
    private void Start()
    {
        if (instance != null) instance = this;
    }
    public void _buyButton()
    {
        if (snakeID == 0)
        {
            Debug.Log("Error");
            return;
        }
        // gameObject.GetComponent<Animator>().Play("PickUp");
        Debug.Log("Spaceshipid: " + snakeID);
        for (int i = 0; i < ShopManager.instance.snakeHeadList.Count; i++)
        {
            // check id
            if (snakeID == ShopManager.instance.snakeHeadList[i].id)
            {
                // check bought
                if (!ShopManager.instance.snakeHeadList[i].bought)
                {
                    Panel.GetComponent<BuyingPanel>().curID = snakeID;                 
                    Instantiate(Panel, null);
                  //  Panel.GetComponent<Animator>().Play("In");
                }
                else
                {
                    PlayerPrefs.SetInt("currentID", snakeID);
                    ShopManager.instance.currentID = snakeID;
                    UpdateBuyButton();
                }
            }
            else
            {

            }
        }
    }
    public void UpdateBuyButton()
    {
        if (ShopManager.instance != null)
            ShopManager.instance.UpdateUI();
    }
}
