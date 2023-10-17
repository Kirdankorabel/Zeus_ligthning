using System.Collections;
using UnityEngine;

public class BoostAbility : Ability
{
    [SerializeField] private float _multiplerValue;
    [SerializeField] private float _bostingTime = 5f;

    public override void Use(Column column)
    {
        base.Use(column);
        StartCoroutine(UseAbilityCorutine(column));
    }

    private IEnumerator UseAbilityCorutine(Column column)
    {
        column.SetIncreaseMultipler(_multiplerValue);
        yield return new WaitForSeconds(_bostingTime);
        column.SetIncreaseMultipler(1f);
    }
}
