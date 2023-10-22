using System;
using UnityEngine;

[Serializable]
public class AbilityInfo
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private string _name;
    [SerializeField] private int _priceFor10;

    public Sprite Sprite => _sprite;
    public string Name => _name;
    public int PriceFor10 => _priceFor10;
}
