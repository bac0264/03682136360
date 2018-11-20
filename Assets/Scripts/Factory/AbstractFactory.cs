using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractFactory : MonoBehaviour {
    public List<GameObject> listPref;
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
        GameObject go = Instantiate(listPref[Random.Range(0, (listPref.Count - 1))]);
        go.transform.position = new Vector3(0, Camera.main.transform.position.y + 6, 0);
        yield return new WaitForSeconds(5f);
        StartCoroutine(generate());
    }
}
