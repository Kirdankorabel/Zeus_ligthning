using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailController : MonoBehaviour
{
    [SerializeField] private LineRenderer _trailRenderer;
    [SerializeField] private float _glowTime;
    [SerializeField] private float _timeStep = 0.01f;
    [SerializeField] private List<Color> _colors = new List<Color>();

    private List<Vector3> _positions = new List<Vector3>();
    private bool _isTracking;

    public List<Color> Colors => _colors;

    public void StartTracking(Transform targetTransform)
    {
        _isTracking = true;
        _positions.Clear();
        StartCoroutine(TrackingCorutine(targetTransform));
    }

    public void EndTracking()
    {
        _isTracking = false;
    }

    public void ThrowLightning(int colorId)
    {
        if (_positions.Count == 0)
            return;
        _isTracking = false;
        _trailRenderer.startColor = _colors[colorId];
        _trailRenderer.endColor = _colors[colorId];

        _trailRenderer.positionCount = _positions.Count;
        _trailRenderer.SetPositions(CalculateLightningPositions());

        StopAllCoroutines();
        StartCoroutine(EnableLightningCorutine());
        
        _positions.Clear();
    }

    private Vector3[] CalculateLightningPositions()
    {
        Vector3[] newPositions = new Vector3[_positions.Count];
        for(var i = 1; i < _positions.Count - 1; i++)
        {
            newPositions[i] = _positions[i] + (_positions[i] - (_positions[i - 1] + _positions[i + 1]) / 2f).normalized * Random.Range(-100, 100);
        }
        newPositions[0] = _positions[0];
        newPositions[_positions.Count - 1] = _positions[_positions.Count - 1];
        return newPositions;
    }

    private IEnumerator EnableLightningCorutine()
    {
        yield return new WaitForSeconds(_glowTime);
        _trailRenderer.positionCount = 0;
    }

    private IEnumerator TrackingCorutine(Transform targetTransform)
    {
        _positions.Add(targetTransform.localPosition);
        while (_isTracking)
        {
            yield return new WaitForSeconds(_timeStep);
            _positions.Add(targetTransform.localPosition);
        }
        _positions.Add(targetTransform.localPosition);
    }
}