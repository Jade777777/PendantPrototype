using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SoulsLike
{
    public class RunState: BaseSoulState
    {
        [SerializeField]
        BehaviorInputType _move;
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
            base.OnEnter();
        }

        protected override void OnExit()
        {
            //throw new System.NotImplementedException();
        }



        public override void OnDash(bool IsActivated)
        {
            if (!IsActivated)
            {
                Core.StateMachine.Transition(_move);
                Debug.Log("STOPING DASH");
            }

        }
        public override void OnStandardAttack()
        {
            Core.StateMachine.Transition(_attack);
        }
        public override void OnHeavyAttack()
        {
            Core.StateMachine.Transition(_heavyAttack);
        }


       
        private void Update()
        {
            UpdatePosition();
            UpdateRotation();
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            Animator animator= GetComponentInChildren<Animator>();
            animator.SetFloat("Velocity", Core.DataTags.GetTag<MovementDataTag>().Velocity.magnitude);

        }



    }
}
