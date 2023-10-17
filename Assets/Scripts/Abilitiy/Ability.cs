using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ability : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private TMP_Text _countText;
    [SerializeField] private string _name;
    [SerializeField] private int _count;
    [SerializeField] private int _priceFor10;

    public event Action<Ability> OnAbilitySelected;
    public event Action<Ability> OnAbilitiesEnded;

    public Sprite Sprite => _sprite;
    public string Name => _name;
    public int Count
    {
        get { return _count; } 
        set 
        {
            _count = value;
            _countText.text = value.ToString();
            PlayerPrefs.SetInt(Name, _count);
        }
    }

    public int PriceFor10 => _priceFor10;

    private void Start()
    {
        Count = PlayerPrefs.GetInt(Name, 5);
    }

    public virtual void Use(Column column)
    {
        if (Count > 0)
            Count--;
    }

    public void AddAbilities(int count)
    {
        Count += count;
    }

    public virtual void Use(Column[] columns)
    {
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (Count == 0 && !GameController.Instance.IsDragged)
        {
            OnAbilitiesEnded?.Invoke(this);
            return;
        }
        OnAbilitySelected?.Invoke(this);
    }
}
