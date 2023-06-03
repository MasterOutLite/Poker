using TMPro;
using UnityEngine;

public class Rate : MonoBehaviour
{
    [SerializeField] private TMP_Text _score;
    [SerializeField] private int _defaultScope = 15;
    [SerializeField] private int _scope = 0;
    public bool Click = true;


    public int Scope { get => _scope; private set { _scope = value; _score.text = _scope.ToString(); } }

    private void Start ()
    {
        Scope = _defaultScope;
    }

    public void ResetRate ()
    {
        Scope = _defaultScope;
        Click = true;
    }

    public void OnIncreasingForScope (int scope)
    {
        if (Click)
        {
            Scope += scope;
            Click = false;
        }           
    }
}
