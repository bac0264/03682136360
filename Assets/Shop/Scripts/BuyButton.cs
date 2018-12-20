using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

        for (int i = 0; i < ShopManager.instance.snakeHeadList.Count; i++)
        {
            // check id
            if (snakeID == ShopManager.instance.snakeHeadList[i].id)
            {
                // check bought
                if (!ShopManager.instance.snakeHeadList[i].bought)
                {
                    Panel.GetComponent<BuyingPanel>().curID = snakeID;
                    Panel.GetComponent<Shop_PopUp>().UpdateUI(int.Parse(ShopManager.instance.snakeHeadList[i].cost));
                    Instantiate(Panel, null);
                  //  Panel.GetComponent<Animator>().Play("In");
                }
                else
                {
                    PlayerPrefs.SetInt("currentID", snakeID);
                    ShopManager.instance.currentID = snakeID;
                    UpdateBuyButton();
                    SaveLoad.instance.saving();
                    SaveLoad.instance.savingStar_2();
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
