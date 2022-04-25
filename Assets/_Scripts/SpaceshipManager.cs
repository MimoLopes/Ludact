using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class SpaceshipManager : PoolingMachine
{
    [SerializeField] private GameObject _spaceshipPrefab;
    [SerializeField] private int _startInstancesQuantity = 1;
    [SerializeField] private float _minVelocity, _maxVelocity, _velcoityIncrease = 0.1f;
    private int[] _fibonacciSequence = new int[99];
    private int _currentFibonacciSequence = 0;
    private Queue<Spaceship> _spaceshipList = new Queue<Spaceship>();

    protected override void Awake()
    {
        base.Awake();

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

            float angle = Calc.RandomAngle();
            Vector3 translationPoint = Calc.RandomPosition(25f);

            newSpaceship.SetPosition(GetSpawnPosition(angle, translationPoint));
            newSpaceship.SetRotation(GetSpawnRotation(angle));
            newSpaceship.SetTranslationPoint(translationPoint);
            newSpaceship.SetVelocity(UnityEngine.Random.Range(_minVelocity, _maxVelocity));
            newSpaceship.SetVelcoityIncrease(_velcoityIncrease);

            _spaceshipList.Enqueue(newSpaceship);

            RecursiveCreateSpaceship(instancesQuantity - 1);
        }
    }

    private Vector3 GetSpawnPosition(float angle, Vector3 position)
    {
        float distance = 25f;

        return (new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * distance) + position;
    }

    private Quaternion GetSpawnRotation(float angle)
    {
        return Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg);
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

