using Mosaic;
using SoulsLike;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: create a base class from which all animation-driven states can derive, with custom functions for entering, exiting, and applying states pre set
public class StaggerState : BaseSoulState 
{
    [SerializeField]
    BehaviorInputType _move;
    [SerializeField]
    ModifierDecorator<DamageMod> _vulnerableMod;
    Guid _vulnerableID;


    [SerializeField]
    float _attackTime = 1.0f;
    protected override void OnEnter()//Enabling and disabling the mod can be put into their own functions to be called by the animator
    {
        base.OnEnter();
        _vulnerableID = Core.Modifiers.AddModifierDecorator(_vulnerableMod);
        Invoke(nameof(EndStagger), _attackTime);
    }
    protected override void OnExit()
    {
        
    }
    public void EndStagger() // Listen for the animator exit instead of requiring the animator to call this function
    {
        Core.Modifiers.RemoveModifierDecorator(_vulnerableID);
        Core.StateMachine.Transition(_move);
    }
   
}
