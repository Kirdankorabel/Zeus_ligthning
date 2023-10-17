using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Counter : Singleton<Counter>
{
    [SerializeField] private TMP_Text _countText;
    [SerializeField] private TMP_Text _bestResultText;
    [SerializeField] private string _countMessage = "Current";
    [SerializeField] private string _bestResultMessage = "Record";
    [SerializeField] private string _playerPrefsItemName = "Record";
    private int _counter;
    private int _bestResult;

    public int BestResult
    {
        get { return _bestResult; }
        private set
        { 
            _bestResult = value;
            _bestResultText.text = $"{_bestResultMessage} {_bestResult}";
        }
    }

    public int Count
    {
        get { return _counter; }
        private set 
        { 
            _counter = value;
            _countText.text = $"{_countMessage} {_counter}";
            if (_counter > BestResult)
            {
                BestResult = _counter;
                PlayerPrefs.SetInt(_playerPrefsItemName, _bestResult);
            }
        }
    }

    private void Awake()
    {
        BestResult = PlayerPrefs.GetInt(_playerPrefsItemName);
    }

    void Start()
    {
        GameController.Instance.OnTargetTouch += () => Increase();
        RestartPanel.Instance.OnRestart += () => Count = 0;
    }

    private void Increase()
    {
        Count++;
    }
}
