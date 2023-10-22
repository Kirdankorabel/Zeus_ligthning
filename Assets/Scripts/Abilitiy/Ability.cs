using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Ability : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private TMP_Text _countText;
    [SerializeField] private Image _icon;
    [SerializeField] private int _count;
    [SerializeField] private AbilityInfo _abilityInfo;

    public event Action<Ability> OnAbilitySelected;
    public event Action<Ability> OnAbilitiesEnded;

    public Sprite Sprite => _abilityInfo.Sprite;
    public string Name => _abilityInfo.Name;
    public int PriceFor10 => _abilityInfo.PriceFor10;
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


    private void Start()
    {
        Count = PlayerPrefs.GetInt(Name, 5);
        _icon.sprite = Sprite;
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
