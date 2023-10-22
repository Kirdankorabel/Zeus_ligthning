using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Lightning : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private Image _image;
    [SerializeField] private int _colorId;
    public int ColorID => _colorId;

    public event Action<int> OnColorIdSelected;
    public event Action OnColorIdDeselected;

    private void Start()
    {
        _image.color = ColumnSettings.Instance.Colors[_colorId];
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnColorIdSelected?.Invoke(_colorId);
    }
}
