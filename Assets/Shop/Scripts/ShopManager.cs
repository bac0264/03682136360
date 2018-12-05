using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;
    public int star;
    public Text starText;
    public List<GameObject> snakeHeadObjectList = new List<GameObject>();
    public List<snakeHeadItem> snakeHeadList = new List<snakeHeadItem>();
    public List<snakeHeadItem> boughtList = new List<snakeHeadItem>();
    public int currentID = 0;
    public List<Sprite> imageHeadList = new List<Sprite>();
    public Transform container;
    public GameObject prefSnakeHeadItem;
    public int count;
    private void Start()
    {
        //Load data

        //
        currentID = PlayerPrefs.GetInt("currentID");
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(prefSnakeHeadItem, container, false);
            obj.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = snakeHeadList[i].cost.ToString();
            obj.transform.GetChild(1).GetComponent<Image>().sprite = imageHeadList[i];
            // xet da mua
            if (snakeHeadList[i].bought)
            {
                obj.transform.GetChild(0).gameObject.SetActive(false);
                // snake chua su dung
                if (snakeHeadList[i].id != currentID)
                {
                    obj.transform.GetChild(2).gameObject.SetActive(false);
                }
                // snake da mua va su dung
                else
                {
                    obj.transform.GetChild(2).gameObject.SetActive(true);
                }
                
            }
            else
            {

            }
            snakeHeadObjectList.Add(obj);
        }
    }
    public void UpdateUI()
    {
        PlayerPrefs.SetInt("Spaceship", currentID);
        for (int i = 0; i < boughtList.Count; i++)
        {
            for (int j = 0; j < snakeHeadList.Count; j++)
            {
                
                if (snakeHeadList[j].bought = boughtList[i].bought)
                {
                    snakeHeadObjectList[j].transform.GetChild(0).gameObject.SetActive(false);
                    // snake item da mua nhung k su dung
                    if (snakeHeadList[j].id != currentID)
                    {
                        snakeHeadObjectList[j].transform.GetChild(2).gameObject.SetActive(false);
                    }
                    // snake da mua va su dung
                    else
                    {
                        snakeHeadObjectList[j].transform.GetChild(2).gameObject.SetActive(true);
                    }
                }               
            }
        }
    }
}
