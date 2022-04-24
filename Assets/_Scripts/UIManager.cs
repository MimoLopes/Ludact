using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Text _createdSpaceshipsText;
    private int _createdSpaceshipsCount;
    [SerializeField] private Text _destroyedSpaceshipsText;
    private int _destroyedSpaceshipsCount;
    [SerializeField] private Text _previousFibonacciSequenceText;
    [SerializeField] private Text _currentFibonacciSequenceText;
    [SerializeField] private Text _nextFibonacciSequenceText;

    public void SetCreatedSpaceshipCount(int numberOfSpaceshipsCreated)
    {
        _createdSpaceshipsCount += numberOfSpaceshipsCreated;
        _createdSpaceshipsText.text = _createdSpaceshipsCount.ToString();
    }
    public void SetDestroyedSpaceshipCount()
    {
        _destroyedSpaceshipsCount++;
        _destroyedSpaceshipsText.text = _destroyedSpaceshipsCount.ToString();
    }

    public void SetPrevioousFibonacciSequence(int previousFibonacciSequence)
    {
        _previousFibonacciSequenceText.text = previousFibonacciSequence.ToString();
    }

    public void SetCurrentFibonacciSequence(int currentFibonacciSequence)
    {
        _currentFibonacciSequenceText.text = currentFibonacciSequence.ToString();
    }

    public void SetNextFibonacciSequence(int nextFibonacciSequence)
    {
        _nextFibonacciSequenceText.text = nextFibonacciSequence.ToString();
    }
}
