using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractFactory : MonoBehaviour
{
    public List<Sprite> listTri;
    public List<Sprite> listHCN;
    public List<Sprite> listSquare;
    public List<Sprite> listCircle;
    public List<GameObject> listPref;
    public Vector3 marked;
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
        float temp = Camera.main.transform.position.y + 15;
        for(int i = 0; i < listPref.Count; i++)
        {
            createPref(temp);
            yield return new WaitForSeconds(2f);
            temp = Camera.main.transform.position.y + 15;
        }
        StartCoroutine(generate());
    }
    void createPref(float value)
    {
        int index = Random.Range(0, (listPref.Count));
        GameObject go = Instantiate(listPref[index]);
        go.transform.position = new Vector3(0, value, 0);
    }
}
