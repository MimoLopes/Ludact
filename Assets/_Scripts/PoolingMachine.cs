using System.Collections.Generic;
using UnityEngine;

public abstract class PoolingMachine : MonoBehaviour
{
    protected Queue<GameObject> _poolQueue = new Queue<GameObject>();

    protected void PopulatePool(GameObject prefab, int instancesQuantity)
    {
        for (int i = 0; i < instancesQuantity; i++)
        {
            CreatePoolObject(prefab);
        }
    }

    protected void CreatePoolObject(GameObject prefab)
    {
        GameObject poolObject = Instantiate(prefab, this.transform);
        SendToPool(poolObject);
    }

    protected void SendToPool(GameObject gameObject)
    {
        _poolQueue.Enqueue(gameObject);
        gameObject.SetActive(false);
    }

    protected GameObject GetFromPool()
    {
        if (_poolQueue.Count > 0)
        {
            GameObject gameObject = _poolQueue.Dequeue();
            gameObject.SetActive(true);

            return gameObject;
        }

        return null;
    }
}
