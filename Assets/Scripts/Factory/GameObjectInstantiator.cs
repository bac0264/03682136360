using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectInstantiator : MonoBehaviour {
    GameObject prefab;
    // Use this for initialization
    public void setPrefab(GameObject prefab)
    {
        this.prefab = prefab;
    }
    public GameObject CreateInstance()
    {
        return Instantiate(prefab);
    }
}
