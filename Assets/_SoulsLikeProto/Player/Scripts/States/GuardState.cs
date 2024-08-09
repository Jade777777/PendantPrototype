using Mosaic;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SoulsLike
{
    public class GuardState : BaseSoulState
    {
        [SerializeField]
        float _guardCounterWindow = 0.8f;

        [SerializeField]
        ModifierDecorator _guardMod;
        Guid _guardID;

        [SerializeField]
        BehaviorInputType _dash;
        [SerializeField]
        BehaviorInputType _dodge;
        [SerializeField]
        BehaviorInputType _attack;
        [SerializeField]
        BehaviorInputType _heavyAttack;
        [SerializeField]
        BehaviorInputType _block;
        [SerializeField]
        BehaviorInputType _useItem;
        [SerializeField]
        BehaviorInputType _interact;
        [SerializeField]
        BehaviorInputType _run;
        [SerializeField]
        BehaviorInputType _parry;
        [SerializeField]
        BehaviorInputType _guardCounter;
        protected override void OnEnter()
        {
            base.OnEnter();

            _guardID = Core.Modifiers.AddModifierDecorator(_guardMod);
        }

        protected override void OnExit()
        {
            Core.Modifiers.RemoveModifierDecorator(_guardID);
        }

        public override void OnDash(bool isActivated)
        {
            if (isActivated)
            {
                Core.StateMachine.Transition(_dash);
            }

        }
        public override void OnDodge()
        {

            Core.StateMachine.Transition(_dodge);

        }
        public override void OnStandardAttack()
        {

            Core.StateMachine.Transition(_attack);
        }
        public override void OnHeavyAttack()
        {
            if (DataTags.GetTag<CombatDataTag>().LastAttackBlockedTime + _guardCounterWindow > Time.time)
            {
                Core.StateMachine.Transition(_guardCounter);
            }
            else
            {
                Core.StateMachine.Transition(_heavyAttack);
            }
        }
        public override void OnGuard(bool isActivated)
        {
            if (!isActivated)
            {
                Core.StateMachine.Transition(_run);
            }
        }


        private void Update()
        {
            UpdatePosition();
            UpdateRotation();
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            Animator animator = GetComponentInChildren<Animator>();
            animator.SetFloat("Velocity", Core.DataTags.GetTag<MovementDataTag>().Velocity.magnitude);

        }



    }

}

