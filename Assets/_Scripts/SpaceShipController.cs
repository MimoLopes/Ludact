using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    private float _velocity;
    public float Velocity { set { _velocity = value; } }
    private float _velcoityIncrease;
    public float VelcoityIncrease { set { _velcoityIncrease = value; } }
    private Vector3 _translationPoint;
    public Vector3 TranslationPoin { set { _translationPoint = value; } }

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        _velocity += _velcoityIncrease * Time.deltaTime;

        transform.RotateAround(_translationPoint, Vector3.forward, _velocity * Time.deltaTime);
    }
}
