using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Column : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image _manaColumn;
    [SerializeField] private int _colorId;

    private float _increaseMultipler = 1f;
    private float _decreaseMultipler = 1f;
    private float _manaCount = 1;

    public event System.Action<Column> OnPointerEnterEvent;
    public event System.Action OnPointerExitEvent;

    public float ManaCount
    {
        get => _manaCount;
        private set
        {
            if (value > 0 && value <= 1f)
                _manaCount = value;
            else if (value < 0f)
                _manaCount = 0f;
            else if (value > 1f)
                _manaCount = 1f;
        }
    }

    private void Start()
    {
        GameController.Instance.OnTargetTouch += (value) => IncreaceMana(ColumnSettings.Instance.IncreaseValue);
        GameController.Instance.OnColorSelected += (colorId) =>
        {
            if (colorId == _colorId)
                DecreaseMana(ColumnSettings.Instance.DecreasePerLightning);
        };
        GameController.Instance.OnColorDeselected += (colorId) =>
        {
            if (colorId == _colorId)
                IncreaceMana(ColumnSettings.Instance.DecreasePerLightning);
        };
        GameController.Instance.OnLose += () => StopAllCoroutines();
        RestartPanel.Instance.OnRestart += () => Restart();
        Restart();
        _manaColumn.color = ColumnSettings.Instance.Colors[_colorId];
    }

    public void Rechange()
    {
        IncreaceMana(1f);
    }

    public void SetIncreaseMultipler(float value)
    {
        _increaseMultipler = value;
    }

    public void SetDecreaseMultipler(float value)
    {
        _decreaseMultipler = value;
    }

    private void Restart()
    {
        StartCoroutine(DecreseCorutine());
        StartCoroutine(ManaChangeCorutine());
        ManaCount = 1f;
    }

    private void IncreaceMana(float value)
    {
        ManaCount += value * _increaseMultipler;
    }

    private void DecreaseMana(float value)
    {
        ManaCount -= value * _decreaseMultipler;
    }


    private IEnumerator DecreseCorutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(ColumnSettings.Instance.DecreaseStepTimeDelay);
            DecreaseMana(ColumnSettings.Instance.DecreaseStep);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnPointerEnterEvent?.Invoke(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnPointerExitEvent?.Invoke();
    }

    private IEnumerator ManaChangeCorutine()
    {
        while (true)
        {
            if (ManaCount > _manaColumn.fillAmount)
                _manaColumn.fillAmount += MathF.Max(Time.deltaTime * 0.02f, (ManaCount - _manaColumn.fillAmount));
            else if (ManaCount < _manaColumn.fillAmount)
                _manaColumn.fillAmount += MathF.Min(Time.deltaTime * 0.001f, (ManaCount - _manaColumn.fillAmount));
            yield return null;
        }
    }

}
