using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mosaic;
using UnityEngine.TextCore.Text;

[CreateAssetMenu(fileName = "BasicDamage", menuName = "Soul / Modifier / BasicDamage", order = 1)]
public class DamageMod : Modifier
{
    [SerializeField]
    int _damage;
    [SerializeField]
    BehaviorInputType _death;
    public override void Begin()
    {
        GetCore().DataTags.GetTag<CombatDataTag>().Health -= _damage;

        Debug.Log("Health: " + GetCore().DataTags.GetTag<CombatDataTag>().Health);
        if (GetCore().DataTags.GetTag<CombatDataTag>().Health <= 0)
        {

            Debug.Log("Dead!");
            GetCore().StateMachine.Transition(_death);
        }
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
