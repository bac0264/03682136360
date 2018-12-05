using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
public class SaveLoad : MonoBehaviour
{

    public class SaveData
    {
        public List<snakeHeadItem> markedList = new List<snakeHeadItem>();
        public List<snakeHeadItem> bought = new List<snakeHeadItem>();
        public int currentSnake;
        public int star;
    }
    public void saving()
    {
        try
        {
            SaveData saveData = new SaveData();
            // Save Data
            for (int i = 0; i < ShopManager.instance.snakeHeadList.Count; i++)
            {
                saveData.markedList.Add(ShopManager.instance.snakeHeadList[i]);
            }
            saveData.currentSnake = ShopManager.instance.currentID;
            saveData.star = ShopManager.instance.star;
            //
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(Application.persistentDataPath + "/shop.txt", FileMode.OpenOrCreate);
            bf.Serialize(fs, saveData);
            fs.Close();
        }
        catch (Exception e)
        {
            print(e);
        }
        print("saved data to " + Application.persistentDataPath + "/shop.txt");
    }
    public void loading()
    {
        Debug.Log(Application.persistentDataPath + "/shop.txt");
        if (File.Exists(Application.persistentDataPath + "/shop.txt"))
        {
            try
            {
                SaveData saveData = new SaveData();
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = new FileStream(Application.persistentDataPath + "/shop.txt", FileMode.Open);
                saveData = (SaveData)bf.Deserialize(fs);
                fs.Close();
                // do somthing
            }
            catch (Exception e)
            {
                print(e);
            }
        }
    }
}
