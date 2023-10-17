using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : Singleton<GameController>
{
    private const int ColorNotSpecified = -1;

    [SerializeField] private TrailController _trailController;
    [SerializeField] private TargetSpawner _targetSpawner;
    [SerializeField] private GameObject _pointer;
    [SerializeField] private Image _pointerImage;
    [SerializeField] private AbilitiesController _abilitiesController;
    [SerializeField] private float _pointToX2 = 20f;
    [SerializeField] private List<Lightning> _lightning;
    [SerializeField] private List<Column> _columns;
    [SerializeField] private List<Color> _pointerColors;

    private int _colorId;
    private bool _dragged;

    public System.Action<int> OnColorSelected;
    public System.Action<int> OnColorDeselected;
    public System.Action OnTargetTouch;
    public System.Action OnLose;

    public bool IsDragged => _dragged;
    public float TimeScale => 1f + (float)Counter.Instance.Count / _pointToX2;

    void Start()
    {
        _colorId = ColorNotSpecified;
        var targets = _targetSpawner.CreateTargets();
        _targetSpawner.OnLosed += () => OnLose?.Invoke();
        RestartPanel.Instance.OnRestart += () => Restart();

        foreach (var target in targets) 
            target.OnTouchEnterToTarget += () => DespawnTarget(target);
        foreach (var lightning in _lightning)
        {
            lightning.OnColorIdSelected += (colorId) => StartGruging(colorId);
            lightning.OnColorIdDeselected += () => ResetColor();
        }
    }

    private void Update()
    {
        CheckMana();
    }

    private void Restart()
    {
        Time.timeScale = 1f;
    }

    private void CheckMana()
    {
        foreach (var column in _columns)
            if (column.ManaCount > 0)
                return;
        OnLose?.Invoke();
    }

    private void StartGruging(int colorId)
    {
        if (_colorId == colorId || _columns[colorId].ManaCount == 0 || _abilitiesController.IsDragged)
            return;
        if (_colorId != ColorNotSpecified)
            OnColorDeselected?.Invoke(_colorId);
        _colorId = colorId;
        OnColorSelected?.Invoke(colorId);

        StartCoroutine(DraggingCorutine());
    }

    private void ResetColor()
    {
        if (!_dragged)
        {
            _colorId = ColorNotSpecified;
        }
    }

    private void DespawnTarget(Target target)
    {
        if (_colorId != target.Color)
            return;
        OnTargetTouch?.Invoke();
        _targetSpawner.DespawnTarget(target);
        _trailController.ThrowLightning(_colorId);
        _colorId = ColorNotSpecified;
        _pointerImage.gameObject.SetActive(false);

        Time.timeScale = 1f + (float)Counter.Instance.Count / _pointToX2;
    }


    private IEnumerator DraggingCorutine()
    {
        _pointerImage.transform.localPosition = Input.mousePosition - new Vector3(Screen.width, Screen.height) / 2f;
        _trailController.StartTracking(_pointerImage.transform);
        _dragged = true;
        _pointerImage.gameObject.SetActive(true);
        _pointerImage.color = _pointerColors[_colorId];

        while (Input.GetMouseButton(0))
        {
            yield return null;
            _pointerImage.transform.localPosition = Input.mousePosition - new Vector3(Screen.width, Screen.height) / 2f;
        }
        OnColorDeselected?.Invoke(_colorId);
        _dragged = false;
        _trailController.EndTracking();
        _colorId = ColorNotSpecified;
        _pointerImage.gameObject.SetActive(false);
        yield break;
    }

}