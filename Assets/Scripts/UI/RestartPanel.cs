using System;
using UnityEngine;
using UnityEngine.UI;

public class RestartPanel : Singleton<RestartPanel>
{
    [SerializeField] private Button _restartButton;

    public event Action OnRestart;

    private void Start()
    {
        GameController.Instance.OnLose += () => gameObject.SetActive(true);
        _restartButton.onClick.AddListener(Restart);
        gameObject.SetActive(false);
    }

    private void Restart()
    {
        OnRestart?.Invoke();
        gameObject.SetActive(false);
    }
}
