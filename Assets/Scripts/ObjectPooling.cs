using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling instance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public Transform ObjectPoolingManager;
    public int amountToPool;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    private void Start()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(objectToPool, ObjectPoolingManager);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }
    public GameObject getObjectPooling()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
    public int amountOfFalse()
    {
        int count = 0;
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                count++;
            }
        }
        return count;
    }
}
