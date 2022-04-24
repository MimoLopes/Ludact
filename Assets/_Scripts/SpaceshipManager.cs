using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class SpaceshipManager : PoolingMachine
{
    [SerializeField] private GameObject _spaceshipPrefab;
    [SerializeField] private int _startInstancesQuantity = 1;
    [SerializeField] private float _minVelocity, _maxVelocity;
    private int[] _fibonacciSequence = new int[99];
    private int _currentFibonacciSequence = 0;
    private Queue<Spaceship> _spaceshipList = new Queue<Spaceship>();

    void Awake()
    {
        PopulatePool(_spaceshipPrefab, CreateNewSpaceShip, _startInstancesQuantity);

        _fibonacciSequence = Calc.FibonacciSequence(_fibonacciSequence.Length);
    }

    void Start()
    {
        StartCoroutine(DestroySpaceship());
    }

    public void OnSpaceshipCreated()
    {
        int quantity = GetFibonacciValue(ref _fibonacciSequence, _currentFibonacciSequence++);

        RecursiveCreateSpaceship(quantity);

        UIManager.Instance.SetCreatedSpaceshipCount(quantity);
        UIManager.Instance.SetPrevioousFibonacciSequence(GetFibonacciValue(ref _fibonacciSequence, _currentFibonacciSequence - 2));
        UIManager.Instance.SetCurrentFibonacciSequence(GetFibonacciValue(ref _fibonacciSequence, _currentFibonacciSequence - 1));
        UIManager.Instance.SetNextFibonacciSequence(GetFibonacciValue(ref _fibonacciSequence, _currentFibonacciSequence));
    }

    private void RecursiveCreateSpaceship(int instancesQuantity)
    {
        if (instancesQuantity > 0)
        {
            if (_poolQueue.Count == 0)
            {
                CreatePoolObject(_spaceshipPrefab, CreateNewSpaceShip);
            }

            Spaceship newSpaceship = GetFromPool() as Spaceship;

            float angle = GetRandomAngle();

            newSpaceship.SetPosition(GetSpawnPosition(angle));
            newSpaceship.SetRotation(GetSpawnRotation(angle));
            newSpaceship.SetVelocity(UnityEngine.Random.Range(_minVelocity, _maxVelocity));

            _spaceshipList.Enqueue(newSpaceship);

            RecursiveCreateSpaceship(instancesQuantity - 1);
        }
    }

    public Vector3 GetSpawnPosition(float angle)
    {
        float distance = UnityEngine.Random.Range(5f, 15f);
        
        float zPosition = UnityEngine.Random.Range(0f, 10f);

        Vector3 position = (new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * distance) + new Vector3(0f, 0f, zPosition);

        return position;
    }

    public Quaternion GetSpawnRotation(float angle)
    {
        return Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg);
    }

    public float GetRandomAngle()
    {
        return UnityEngine.Random.Range(-Mathf.PI, Mathf.PI);
    }

    private PoolObject CreateNewSpaceShip(GameObject associatedGameObject)
    {
        Spaceship newSpaceShip = new Spaceship(associatedGameObject);

        return newSpaceShip;
    }

    private int GetFibonacciValue(ref int[] fibonacciSequence, int sequenceTarget)
    {
        if (sequenceTarget < 0)
        {
            return 0;
        }

        if (fibonacciSequence.Length <= sequenceTarget)
        {
            Array.Resize(ref fibonacciSequence, fibonacciSequence.Length + 1);

            fibonacciSequence[sequenceTarget] = fibonacciSequence[fibonacciSequence.Length - 3] + fibonacciSequence[fibonacciSequence.Length - 1];
        }

        return fibonacciSequence[sequenceTarget];
    }

    private IEnumerator DestroySpaceship()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            if (_spaceshipList.Count > 0)
            {

                SendToPool(_spaceshipList.Dequeue());

                UIManager.Instance.SetDestroyedSpaceshipCount();
            }
        }
    }
}

