using Mosaic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SoulsLike
{
    public class GuardCounterState : BaseSoulState
    {
       
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
            Invoke("EndDash", _attackTime);
        }
        private void EndDash()
        {
            Character.StateMachine.Transition(_nextInput);
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

        private void LateUpdate()
        {
            if (IsPlayer)
            {
                UpdateCamera();
            }
        }





    }
}
