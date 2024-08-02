using Mosaic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "BasicParry", menuName = "Soul / Modifier / Parry", order = 1)]
public class ParryMod : ModifierDecorator<DamageMod>
{
    [SerializeField]
    float _parryAngle = 45;
    [SerializeField]
    Modifier _staggerMod;//the modifier that is applied when a target is succesfuly parried.
    public override void Begin()
    {
        Transform attacker = GetOrigin().StateMachine.GetCurrentStateInstance().transform;
        Transform defender = GetCore().StateMachine.GetCurrentStateInstance().transform;
        //Check to see if the defender is facing the attacker
        Vector3 offset= attacker.position - defender.position;
        float angleOfAttack = Vector3.Angle(offset, defender.forward);
        Debug.Log("angle of attack "+ angleOfAttack);

        
        if (angleOfAttack < _parryAngle)
        {
            Debug.Log("PARRY SUCCESFUL");
            ICore origin = GetOrigin(); //The enemy that is currently attacking the player
            origin.Modifiers.ApplyModifier(_staggerMod,GetCore());
        }
    }
}
