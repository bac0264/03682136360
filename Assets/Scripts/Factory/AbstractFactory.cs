using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractFactory : MonoBehaviour
{
    public List<Sprite> listTri;
    public List<Sprite> listCircle;
    public List<Sprite> listHCN;
    public List<Sprite> listSquare;
    public List<Sprite> listChangeColor;
    public List<GameObject> listPref;
    public int marked;
    public Sprite Star;
    public List<string> listTag;
    public List<int> listLayer;
    public GameObject headSnake;
    public int getIndexTagOfObject(string tag)
    {
        for (int i = 0; i < listTag.Count; i++)
        {
            if (tag.Equals(listTag[i])) return i;
        }
        return -1;
    }
    public int getIndexLayerOfObject(int layer)
    {
        for (int i = 0; i < listLayer.Count; i++)
        {
            if (layer == listLayer[i]) return i;
        }
        return -1;
    }
    public bool checkCC(GameObject prefab)
    {
        for (int i = 0; i < prefab.transform.childCount; i++)
        {
            if (prefab.transform.GetChild(i).gameObject.layer == 12) return true;
        }
        return false;
    }
    public int posOfCC(GameObject prefab)
    {
        for (int i = 0; i < prefab.transform.childCount; i++)
        {
            if (prefab.transform.GetChild(i).gameObject.layer == 12) return i;
        }
        return 0;
    }

    private void Start()
    {
        marked = -1;
        StartCoroutine(generate());
    }
    // tạo ra cục pref to
    IEnumerator generate()
    {
        float temp = Camera.main.transform.position.y + 10;
        for(int i = 0; i < listPref.Count; i++)
        {
            createPref(temp);
            yield return new WaitForSeconds(3f);
            temp = Camera.main.transform.position.y + 12;
        }
        StartCoroutine(generate());
    }
    public void createPref(float value)
    {
        int index = Random.Range(0, (listPref.Count));
        GameObject go = Instantiate(listPref[index]);
        go.transform.position = new Vector3(0, value, 0);
        randomGameObject(go);
    }
    // tạo ra các cục nhỏ và thêm tag + layer +sprite
    public void randomGameObject(GameObject prefabs)
    {
        // Check child
        int count = prefabs.transform.childCount;
        if (count < 1) return;
        else
        {
            bool check = checkCC(prefabs);
            Transform childs = prefabs.transform;
            // vi tri se trung mau voi barrier cc
            int randomIndex = Random.Range(1, count);
            while(childs.GetChild(randomIndex).gameObject.layer == 13)
            {
                randomIndex = Random.Range(1, count);
            }
            if (check)
            { // vi tri trung mau voi CC
                for (int i = 0; i < count; i++)
                {
                    int random = Random.Range(0, listTri.Count);
                    if (i == 0) {
                        int colorOfSnake = getIndexTagOfObject(headSnake.tag);
                        while (colorOfSnake == random)
                        {
                            random = Random.Range(0, listTri.Count);
                        }
                        marked = random;
                    }
                    else
                    {
                        if (i == randomIndex) random = marked;
                        else
                        {
                            while(random == marked)
                            {
                                random = Random.Range(0, listTri.Count);
                            }
                        }
                    }
                    findingObject(childs, i, random);
                }
            }
            // cung mau voi ran
            else
            {
                Debug.Log("snake Tag" + headSnake.tag);
                for (int i = 0; i < count; i++)
                {
                    int random = Random.Range(0, listTri.Count);
                    Debug.Log("random truoc" + i + ": " + random);
                    if (i == 0)
                    {
                        if (marked < 0 )
                        {
                            random = getIndexTagOfObject(headSnake.tag);
                            marked = random;
                            Debug.Log("Marked _ 1: " + marked);
                        }
                        else
                            random = marked;
                        Debug.Log("Marked _ 2: " + marked);
                    }
                    else
                    {
                        if (i == randomIndex) random = marked;
                        else
                        {
                            while (random == marked)
                            {
                                random = Random.Range(0, listTri.Count);
                            }
                        }
                    }
                    Debug.Log("random sau" + i + ": " + random);
                    findingObject(childs, i, random);
                }
            }
        }
        return;
    }
    // Get Index Tag
 
    //listTri; 0
    //listCircle; 1
    //listHCN; 2 
    //listSquare; 3
    public void findingObject(Transform childs, int i, int random)
    {
        // index de tim sprite can thay doi trong list tren
        int _index = getIndexLayerOfObject(childs.GetChild(i).gameObject.layer);
        // Change color
        switch (_index)
        {
            case 0: // setup sprite, tag for object
                childs.GetChild(i).GetComponent<SpriteRenderer>().sprite = listTri[random];
                childs.GetChild(i).tag = listTag[random];
                break;
            case 1:
                childs.GetChild(i).GetComponent<SpriteRenderer>().sprite = listCircle[random];
                childs.GetChild(i).tag = listTag[random];
                break;
            case 2:
                childs.GetChild(i).GetComponent<SpriteRenderer>().sprite = listHCN[random];
                childs.GetChild(i).tag = listTag[random];
                break;
            case 3:
                childs.GetChild(i).GetComponent<SpriteRenderer>().sprite = listSquare[random];
                childs.GetChild(i).tag = listTag[random];
                break;
            case 4:
                childs.GetChild(i).GetComponent<SpriteRenderer>().sprite = listChangeColor[random];
                childs.GetChild(i).tag = listTag[random];
                break;
            default:
                childs.GetChild(i).GetComponent<SpriteRenderer>().sprite = Star;
                childs.GetChild(i).gameObject.layer = 13;
                break;
        }
    }
}
