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
    public Vector3 marked;
    public List<string> listTag;
    public List<int> listLayer;
    // private GameObjectInstantiator instantiator;
    /* public AbstractFactory(GameObject prefab)
     {
         instantiator = gameObject.AddComponent(typeof(GameObjectInstantiator)) as GameObjectInstantiator;
         Debug.Log(instantiator);
         instantiator.setPrefab(prefab);
     }*/

    /* private void Start()
     { 
         instantiator.CreateInstance();
     }*/
    private void Start()
    {
        StartCoroutine(generate());
    }
    IEnumerator generate()
    {
        float temp = Camera.main.transform.position.y + 10;
        for(int i = 0; i < listPref.Count; i++)
        {
            createPref(temp);
            yield return new WaitForSeconds(3f);
            temp = Camera.main.transform.position.y + 10;
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
    // Random Tag and Type of Object ( replace Sprite)
    public void randomGameObject(GameObject prefabs)
    {
        // Check child
        int count = prefabs.transform.childCount;
        if (count < 1) return;
        else
        {
            Transform childs = prefabs.transform;
            for (int i = 0; i < count; i++)
            {
                findingObject(childs, i);
            }
        }
        return;
    }
    // Get Index Tag
    public int getIndexTagOfObject(string tag)
    {
        for (int i = 0; i < listTag.Count; i++)
        {
            if (tag == listTag[i]) return i;
        }
        return 0;
    }
    public int getIndexLayerOfObject(int layer)
    {
        for (int i = 0; i < listLayer.Count; i++)
        {
            if (layer == listLayer[i]) return i;
        }
        return 0;
    }
    //listTri; 0
    //listCircle; 1
    //listHCN; 2 
    //listSquare; 3
    public void findingObject(Transform childs, int i)
    {
        // index de tim sprite can thay doi trong list tren
        int _index = getIndexLayerOfObject(childs.GetChild(i).gameObject.layer);
        Debug.Log("index:"+_index);
        Debug.Log("Layer: " + childs.GetChild(i).gameObject.layer.ToString());
        int random = Random.Range(0, listTri.Count );
        Debug.Log("random: " + random);
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
                break;
        }
    }
}
