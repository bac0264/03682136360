using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shop_PopUp : MonoBehaviour {
    public Text starText;

    public void UpdateUI(int star)
    {
        starText.text = star.ToString();
    }
}
