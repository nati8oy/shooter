using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance; // Singleton instance

    public Dictionary<GameObject, List<GameObject>> pooledObjects = new Dictionary<GameObject, List<GameObject>>();
    public List<ObjectPoolItem> itemsToPool;

    void Awake()
    {
        Instance = this;

        // Initialize pooled objects dictionary
        pooledObjects = new Dictionary<GameObject, List<GameObject>>();

        // Create object pools
        foreach (ObjectPoolItem item in itemsToPool)
        {
            CreateObjectPool(item);
        }
    }

    private void CreateObjectPool(ObjectPoolItem item)
    {
        List<GameObject> objectPool = new List<GameObject>();
        for (int i = 0; i < item.amountToPool; i++)
        {
            GameObject obj = Instantiate(item.objectToPool);
            obj.SetActive(false);
            objectPool.Add(obj);
        }
        pooledObjects.Add(item.objectToPool, objectPool);
    }

    public GameObject GetPooledObject(GameObject objectType)
    {
        if (pooledObjects.ContainsKey(objectType))
        {
            foreach (GameObject obj in pooledObjects[objectType])
            {
                if (!obj.activeInHierarchy)
                {
                    return obj;
                }
            }
        }
        return null;
    }
}

[System.Serializable]
public class ObjectPoolItem
{
    public GameObject objectToPool;
    public int amountToPool;
}
