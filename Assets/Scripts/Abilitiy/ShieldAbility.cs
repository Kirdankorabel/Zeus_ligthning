using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAbility : Ability
{
    [SerializeField] private float _multiplerValue;
    [SerializeField] private float _bostingTime = 5f;

    public override void Use(Column[] columns)
    {
        base.Use(columns);
        foreach (Column column in columns)
        {
            StartCoroutine(UseAbilityCorutine(column));
        }
    }

    private IEnumerator UseAbilityCorutine(Column column)
    {
        column.SetDecreaseMultipler(_multiplerValue);
        yield return new WaitForSeconds(_bostingTime);
        column.SetDecreaseMultipler(1f);
    }
}
