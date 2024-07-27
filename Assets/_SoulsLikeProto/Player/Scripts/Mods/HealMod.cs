using Mosaic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BasicHeal", menuName = "Soul / Modifier / BasicHeal", order = 1)]
public class HealMod : Modifier
{
    [SerializeField]
    int _heal = 4;

    public override void Begin()
    {

        GetCore().DataTags.GetTag<CombatDataTag>().Health =
            Mathf.Clamp(GetCore().DataTags.GetTag<CombatDataTag>().Health + _heal, 0, GetCore().DataTags.GetTag<CombatDataTag>().MaxHealth);
        Debug.Log("Healed! " + GetCore().DataTags.GetTag<CombatDataTag>().HealthPots + " health pots remaining");
    }

    public override void End()
    {

    }

    public override bool EndCondition()
    {
        return false;
    }

    public override void Tick()
    {

    }
}
