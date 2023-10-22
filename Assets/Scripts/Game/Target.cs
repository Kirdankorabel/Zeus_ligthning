using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Target : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private Image _image;
    [SerializeField] private RectTransform _rectTransform;
    public event Action OnTouchEnterToTarget;
    private int _colorId = -1;

    public int Color => _colorId;

    public void SetColor(Color color, int colorId)
    {
        _image.color = color;
        _colorId = colorId;
    }

    public void SetPosition(Vector3 position)
    {
        _rectTransform.localPosition = position;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnTouchEnterToTarget?.Invoke();
    }
}
