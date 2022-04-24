using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class SpaceshipManager : PoolingMachine
{
    [SerializeField] private GameObject _spaceshipPrefab;
    [SerializeField] private int _startInstancesQuantity = 1;
    private int[] _fibonacciSequence = new int[99];
    private int _currentFibonacciSequence = 0;
    private Queue<GameObject> _spaceshipList = new Queue<GameObject>();
    private static SpaceshipManager _instance;
    public static SpaceshipManager Instance { get { return _instance; } }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        PopulatePool(_spaceshipPrefab, _startInstancesQuantity);

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
                CreatePoolObject(_spaceshipPrefab);
            }

            _spaceshipList.Enqueue(GetFromPool());

            RecursiveCreateSpaceship(instancesQuantity - 1);
        }
    }

    private int GetFibonacciValue(ref int[] fibonacciSequence, int sequenceTarget)
    {
        if(sequenceTarget < 0)
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

