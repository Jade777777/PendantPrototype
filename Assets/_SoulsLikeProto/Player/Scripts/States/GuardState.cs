using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SoulsLike
{
    public class GuardState : BaseSoulState
    {

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
        protected override void OnEnter()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        protected override void OnExit()
        {
            //throw new System.NotImplementedException();
        }

        public override void OnMove(Vector2 Input)
        {
            DataTags.GetTag<MovementDataTag>().Direction = Input;
        }
        public override void OnDash(bool isActivated)
        {
            if (isActivated)
            {
                Character.StateMachine.Transition(_dash);
            }

        }
        public override void OnDodge()
        {

            Character.StateMachine.Transition(_dodge);

        }
        public override void OnStandardAttack()
        {
            Character.StateMachine.Transition(_attack);
        }
        public override void OnHeavyAttack()
        {
            Character.StateMachine.Transition(_heavyAttack);
        }
        public override void OnGuard(bool isActivated)
        {
            if (!isActivated)
            {
                Character.StateMachine.Transition(_run);
            }
        }
        public override void OnGuardCounter()
        {
            Character.StateMachine.Transition(_parry);
        }

        private void Update()
        {
            UpdatePosition();
            UpdateRotation();
            UpdateAnimator();
        }
        private void LateUpdate()
        {
            if (IsPlayer)
            {
                UpdateCamera();
            }
        }

        private void UpdateAnimator()
        {
            Animator animator = GetComponentInChildren<Animator>();
            animator.SetFloat("Velocity", Character.DataTags.GetTag<MovementDataTag>().Velocity.magnitude);

        }



    }

}

