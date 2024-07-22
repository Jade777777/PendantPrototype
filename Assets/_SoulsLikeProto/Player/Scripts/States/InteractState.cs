using Mosaic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SoulsLike
{
    public class InteractState : BaseSoulState
    {
       
        [SerializeField]
        float _attackTime = 0.5f;

        [SerializeField] 
        BehaviorInputType _nextInput;

        protected override void OnEnter()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Invoke("EndDash", _attackTime);
        }
        private void EndDash()
        {
            Character.StateMachine.Transition(_nextInput);
        }
        protected override void OnExit()
        { }



    }
}
