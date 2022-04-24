using UnityEngine;

public abstract class PoolObject
{
    protected GameObject _associatedGameObject;
    public GameObject AssociatedGameObject { get { return _associatedGameObject; } }

    protected PoolObject(GameObject gameObject)
    {
        _associatedGameObject = gameObject;
    }

}
