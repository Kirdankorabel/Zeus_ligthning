using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RechangeAbility : Ability
{
    public override void Use(Column column)
    {
        base.Use(column);
        column.Rechange();
    }
}
