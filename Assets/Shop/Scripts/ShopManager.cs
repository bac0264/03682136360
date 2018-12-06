using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;
    public int star;
    public Text starText;
    public List<GameObject> snakeHeadObjectList = new List<GameObject>();
    public List<snakeHeadItem> snakeHeadList = new List<snakeHeadItem>();
    public List<snakeHeadItem> boughtList = new List<snakeHeadItem>();
    public int currentID;
    public List<Sprite> imageHeadList = new List<Sprite>();
    public Transform container;
    public GameObject prefSnakeHeadItem;
    public int count;
    private void Awake()
    {
        if (instance == null) instance = this;
        _IsGameStartedForTheFirstTime();
    }
    private void Start()
    {
        //Load da   ta

        //
        if(snakeHeadList != null)
        {
            snakeHeadList[0].bought = true;
        }
        currentID = PlayerPrefs.GetInt("currentID");
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(prefSnakeHeadItem, container, false);
            obj.transform.GetChild(2).GetChild(1).GetComponent<Text>().text = snakeHeadList[i].cost.ToString();
            obj.transform.GetChild(1).GetComponent<Image>().sprite = imageHeadList[i];
            obj.transform.GetChild(2).GetComponent<BuyButton>().snakeID = snakeHeadList[i].id;
            // xet da mua
            if (snakeHeadList[i].bought)
            {
                obj.transform.GetChild(2).gameObject.SetActive(false);
                // snake chua su dung
                if (snakeHeadList[i].id != currentID)
                {
                    obj.transform.GetChild(0).gameObject.SetActive(false);
                }
                // snake da mua va su dung
                else
                {
                    obj.transform.GetChild(0).gameObject.SetActive(true);
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
        PlayerPrefs.SetInt("currentID", currentID);
        for (int i = 0; i < boughtList.Count; i++)
        {
            for (int j = 0; j < snakeHeadList.Count; j++)
            {
                
                if (snakeHeadList[j].bought = boughtList[i].bought)
                {
                    snakeHeadObjectList[j].transform.GetChild(2).gameObject.SetActive(false);
                    // snake item da mua nhung k su dung
                    if (snakeHeadList[j].id != currentID)
                    {
                        snakeHeadObjectList[j].transform.GetChild(0).gameObject.SetActive(false);
                    }
                    // snake da mua va su dung
                    else
                    {
                        snakeHeadObjectList[j].transform.GetChild(0).gameObject.SetActive(true);
                    }
                }               
            }
        }
    }

    public void addMoney(int amount)
    {
        star += amount;
        UpdateStar();
    }
    public void reduceStar(int amount)
    {
        star -= amount;
        UpdateStar();
    }
    public bool requestStar(int amount)
    {
        if (amount <= star) return true;
        return false;
    }
    void UpdateStar()
    {
        starText.text = star.ToString();
    }
    void _IsGameStartedForTheFirstTime()
    {

        if (!PlayerPrefs.HasKey("IsGameStartedForTheFirstTime"))
        {
            PlayerPrefs.SetInt("currentID", 1);
            PlayerPrefs.SetInt("IsGameStartedForTheFirstTime", 0);
        }

    }
    public void Home()
    {
        SceneManager.LoadScene("Menu");
    }
}
