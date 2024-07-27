using Mosaic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SoulsLike
{
    public class AttackState : BaseSoulState
    {
       
        [SerializeField]
        float _attackTime = 0.5f;


        [SerializeField]
        BehaviorInputType _nextInput;

        protected override void OnEnter()
        {
            base.OnEnter();
            Invoke("EndDash", _attackTime);
        }
        private void EndDash()
        {
            Core.StateMachine.Transition(_nextInput);
        }
        protected override void OnExit()
        { }






    }
}
