using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SoulsLike
{
    public class MoveState : BaseSoulState
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
        protected override void OnEnter()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        protected override void OnExit()
        {
            //throw new System.NotImplementedException();
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
            if (isActivated)
            {
                Character.StateMachine.Transition(_block);
            }
        }
        public override void OnUseItem()
        {
            Character.StateMachine.Transition(_useItem);
        }
        public override void OnInteract()
        {
            Character.StateMachine.Transition(_interact);
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
            animator.SetFloat("Velocity", Character.DataTags.GetTag<MovementDataTag>().Velocity.magnitude/_speed);
        }


    }

}

