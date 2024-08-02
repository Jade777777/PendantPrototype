using Mosaic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VulnerableMod", menuName = "Soul / Modifier / VulnerableMod", order = 1)]
public class VulnerabeMod : ModifierDecorator<DamageMod>
{
    [SerializeField]
    float _criticalAngle = 45;//TODO: Replace critical angle with crit points
    [SerializeField]
    int _critDamage = 10;
    public override void Begin()
    {
        Transform attacker = GetOrigin().StateMachine.GetCurrentStateInstance().transform;
        Transform defender = GetCore().StateMachine.GetCurrentStateInstance().transform;
        //Check to see if the defender is facing the attacker
        Vector3 offset = attacker.position - defender.position;
        float angleOfAttack = Vector3.Angle(offset, defender.forward);
        Debug.Log("angle of attack " + angleOfAttack);


        if (angleOfAttack < _criticalAngle)
        {
            Debug.Log("Critical!");
            GetCore().DataTags.GetTag<CombatDataTag>().Health -= _critDamage;
            base.Begin(); 
        }
    }
}
