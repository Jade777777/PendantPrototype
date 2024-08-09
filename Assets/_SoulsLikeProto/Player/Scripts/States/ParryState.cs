using Mosaic;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SoulsLike
{
    public class ParryState : BaseSoulState
    {
        [SerializeField]
        ModifierDecorator<DamageMod> _parryMod;
        Guid _parryID;

        [SerializeField]
        float _attackTime = 0.5f;


        [SerializeField]
        BehaviorInputType _nextInput;
        [SerializeField]
        BehaviorInputType _dash;
        [SerializeField]
        BehaviorInputType _block;
        protected override void OnEnter()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _parryID = Core.Modifiers.AddModifierDecorator(_parryMod);
            Debug.Log("PARRYYY!!!!!!");
            Invoke(nameof(EndDash), _attackTime);

        }
        private void EndDash()
        {
            Core.Modifiers.RemoveModifierDecorator(_parryID);
            Core.StateMachine.Transition(_nextInput);
        }
        protected override void OnExit()
        { }

        public override void OnGuard(bool isActivated)
        {
            _nextInput = isActivated ? _dash : _block;    
        }

        private void Update()
        {
        }






    }
}
