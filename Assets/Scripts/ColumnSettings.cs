using System.Collections.Generic;
using UnityEngine;

public class ColumnSettings : Singleton<ColumnSettings>
{
    [SerializeField] private float _decreaseStepTimeDelay = 1f;
    [SerializeField] private float _decreaseStep = 0.01f;
    [SerializeField] private float _decreasePerLightning = 0.05f;
    [SerializeField] private float _increaseValue = 0.05f;
    [SerializeField] private List<Color> _colors = new List<Color>();

    public float DecreaseStepTimeDelay => _decreaseStepTimeDelay;
    public float IncreaseValue => _increaseValue;
    public float DecreaseStep => _decreaseStep;
    public float DecreasePerLightning => _decreasePerLightning;
    public List<Color> Colors => _colors;
}
