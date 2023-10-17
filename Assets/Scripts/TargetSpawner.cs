using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField] private Target _targetPrefab;
    [SerializeField] private int _targetCount = 3;
    [SerializeField] private float _respawnTimeDelay = 1f;
    [SerializeField] private RectTransform _spawnRectTransform;
    [SerializeField] private List<Color> _colors = new List<Color>()
    {
        Color.red,
        Color.green,
        Color.blue
    };

    private List<Target> _targets = new List<Target>();
    private Rect _spawnArea;

    public event System.Action OnLosed;

    private void Start()
    {
        GameController.Instance.OnLose += () => StopAllCoroutines();
        RestartPanel.Instance.OnRestart += () => ClearTargets();
    }

    public List<Target> CreateTargets()
    {
        _spawnArea = _spawnRectTransform.rect;
        Target target;
        for(var i = 0; i < _targetCount; i++)
        {
            target = Instantiate(_targetPrefab, transform);
            target.gameObject.SetActive(false);
            _targets.Add(target);
        }
        StartCoroutine(TargetSpawnCorutine());
        return _targets;
    }

    public void DespawnTarget(Target target)
    {
        target.gameObject.SetActive(false);
    }

    private void ClearTargets()
    {
        foreach (var target in _targets)
            DespawnTarget(target);
        StartCoroutine(TargetSpawnCorutine());
    }

    private void SpawnTarget()
    {
        var target = GetTarget();

        if (target == null) 
        {
            OnLosed?.Invoke();
            return;
        }

        Vector2 position = new Vector2(Random.Range(_spawnArea.xMin, _spawnArea.xMax), Random.Range(_spawnArea.yMin, _spawnArea.yMax));
        target.SetPosition(position);
        var colorId = Random.Range(0, _colors.Count);
        target.SetColor(_colors[colorId], colorId);
        target.gameObject.SetActive(true);
    }

    private Target GetTarget()
    {
        for(var i = 0; i < _targets.Count; i++)
        {
            if (!_targets[i].gameObject.activeSelf)
                return _targets[i];
        }
        return null;
    }

    private IEnumerator TargetSpawnCorutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(_respawnTimeDelay);
            SpawnTarget();
        }
    }
}
