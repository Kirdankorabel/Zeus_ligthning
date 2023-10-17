using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private GameObject _pausePanel;

    void Start()
    {
        _pauseButton.onClick.AddListener(StartPause);
        _resumeButton.onClick.AddListener(Resume);
        Time.timeScale = 0f;
    }

    private void StartPause()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
    }

    private void Resume()
    {
        Time.timeScale = GameController.Instance.TimeScale;
        gameObject.SetActive(false);
    }
}
