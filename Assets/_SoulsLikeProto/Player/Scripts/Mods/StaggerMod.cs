using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mosaic;
[CreateAssetMenu(fileName = "BasicStagger", menuName = "Soul / Modifier / Stagger", order = 1)]
public class StaggerMod : Modifier
{
    [SerializeField]
    BehaviorInputType _stagger;
    public override void Begin()
    {
        GetCore().StateMachine.Transition(_stagger);
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
