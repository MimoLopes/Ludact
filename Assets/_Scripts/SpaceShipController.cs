using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    [SerializeField] private float _velocity;
    [SerializeField]private Vector3 _translationPosition;
    [SerializeField]private Vector3 _nexTranslationPosition;
    private IEnumerator _increaseVelocity;

    void Awake()
    {
        _increaseVelocity = IncreaseVelocity();
    }

    void OnEnable()
    {
        _translationPosition = new Vector3(0f, 0f, transform.position.z);
        _nexTranslationPosition = _translationPosition;

        StartCoroutine(_increaseVelocity);
    }

    void OnDisable()
    {
        StopCoroutine(_increaseVelocity);
    }

    void Update()
    {
        Translate();
    }

    public void SetVelocity(float velocity)
    {
        _velocity = velocity;
    }

    private void Translate()
    {
        _translationPosition = GetTranslationTarget(_translationPosition, ref _nexTranslationPosition);

        transform.RotateAround(_translationPosition, Vector3.forward, _velocity * Time.deltaTime);
    }

    private Vector3 GetTranslationTarget(Vector3 translationPosition, ref Vector3 nextTranslationPosition)
    {
        if (translationPosition == nextTranslationPosition)
        {
            float x, y, z;

            x = Random.Range(translationPosition.x - 1, translationPosition.x + 1);
            y = Random.Range(translationPosition.y - 1, translationPosition.y + 1);
            z = transform.position.z;

            nextTranslationPosition = new Vector3(x, y, z);
        }

        return Vector3.MoveTowards(translationPosition, nextTranslationPosition, _velocity * Time.deltaTime);
    }

    private IEnumerator IncreaseVelocity()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            _velocity+= 0.1f;
        }
    }

}
