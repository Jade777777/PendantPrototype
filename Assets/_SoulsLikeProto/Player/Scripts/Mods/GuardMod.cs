using Mosaic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicGuard", menuName = "Soul / Modifier / Guard", order = 1)]
public class GuardMod : ModifierDecorator<DamageMod>
{
    [SerializeField]
    float _blockAngle;
    public override void Begin()
    {
        Transform attacker = GetOrigin().StateMachine.GetCurrentStateInstance().transform;
        Transform defender = GetCore().StateMachine.GetCurrentStateInstance().transform;
        //Check to see if the defender is facing the attacker
        Vector3 offset= attacker.position - defender.position;
        float angleOfAttack = Vector3.Angle(offset, defender.forward);
        Debug.Log("angle of attack "+ angleOfAttack);

        if (angleOfAttack > _blockAngle)
        {
            base.Begin();
        }
    }
}
