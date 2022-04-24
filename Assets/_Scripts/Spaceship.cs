using UnityEngine;

public class Spaceship : PoolObject
{
    private readonly SpaceshipController _spaceShipController;
    public Spaceship(GameObject gameObject) : base(gameObject)
    {
        _spaceShipController = gameObject.GetComponent<SpaceshipController>();
    }

    public void SetPosition(Vector3 position)
    {
        _associatedGameObject.transform.position = position;
    }

    public void SetRotation(Quaternion rotation)
    {
        _associatedGameObject.transform.rotation = rotation;
    }

    public void SetVelocity(float velocity)
    {
        _spaceShipController.SetVelocity(velocity);
    }
}
