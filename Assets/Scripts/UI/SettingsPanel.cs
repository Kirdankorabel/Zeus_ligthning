using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private string _prefsName = "volume";

    void Start()
    {
        _settingsButton.onClick.AddListener(OpenSettingsPanel);
        _closeButton.onClick.AddListener(CloseSettingsPanel);
        GameController.Instance.OnTargetTouch += (value) => _audioSource.Play();
        _volumeSlider.onValueChanged.AddListener((value) => _audioSource.volume = value);
        gameObject.SetActive(false);
        _volumeSlider.value = PlayerPrefs.GetFloat(_prefsName, 1);
    }

    private void OpenSettingsPanel()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
    }

    private void CloseSettingsPanel()
    {
        Time.timeScale = GameController.Instance.TimeScale;
        gameObject.SetActive(false);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat(_prefsName, _volumeSlider.value);
    }
}
