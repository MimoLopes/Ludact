using System.Collections.Generic;
using UnityEngine;

public abstract class PoolingMachine : Singleton<PoolingMachine>
{
    protected Queue<PoolObject> _poolQueue = new Queue<PoolObject>();
    protected delegate PoolObject GetNewPoolObject(GameObject AssociatedGameObject);

    protected void PopulatePool(GameObject prefab, GetNewPoolObject getNewPoolObject, int instancesQuantity)
    {
        for (int i = 0; i < instancesQuantity; i++)
        {
            CreatePoolObject(prefab, getNewPoolObject);
        }
    }

    protected void CreatePoolObject(GameObject prefab, GetNewPoolObject getNewPoolObject)
    {
        GameObject associatedGameObject = Instantiate(prefab, this.transform);
        PoolObject poolObject = getNewPoolObject(associatedGameObject);
        SendToPool(poolObject);
    }

    protected void SendToPool(PoolObject poolObject)
    {
        _poolQueue.Enqueue(poolObject);
        poolObject.AssociatedGameObject.SetActive(false);
    }

    protected PoolObject GetFromPool()
    {
        if (_poolQueue.Count > 0)
        {
            PoolObject poolObject = _poolQueue.Dequeue();
            poolObject.AssociatedGameObject.SetActive(true);

            return poolObject;
        }

        return null;
    }
}
