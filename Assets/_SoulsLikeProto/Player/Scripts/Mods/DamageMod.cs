using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mosaic;

[CreateAssetMenu(fileName = "BasicDamage", menuName = "Soul / Modifier / BasicDamage", order = 1)]
public class DamageMod : Modifier
{
    [SerializeField]
    int _damage; 
    public override void Begin()
    {
        this.GetCore().DataTags.GetTag<CombatDataTag>().Health -= _damage;
    }

    public override void End()
    {
        //throw new System.NotImplementedException();
    }

    public override bool EndCondition()
    {
        //throw new System.NotImplementedException();
        return false;
    }

    public override void Tick()
    {
        //throw new System.NotImplementedException();
    }


}
