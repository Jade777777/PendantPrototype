using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SoulsLike
{
    public class RunState: BaseSoulState
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

        public override void OnMove(Vector2 Input)
        {
            DataTags.GetTag<MovementDataTag>().Direction = Input;
            //_moveInput = Input;
        }

        public override void OnDash(bool IsActivated)
        {
            if (!IsActivated)
            {
                Character.StateMachine.Transition(_dash);
            }
        }
        public override void OnStandardAttack()
        {
            Character.StateMachine.Transition(_attack);
        }
        public override void OnHeavyAttack()
        {
            Character.StateMachine.Transition(_heavyAttack);
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
            Animator animator= GetComponentInChildren<Animator>();
            animator.SetFloat("Velocity", Character.DataTags.GetTag<MovementDataTag>().Velocity.magnitude);

        }



    }
}
