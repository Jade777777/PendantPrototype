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
            Character.StateMachine.Transition(_nextInput);
        }
        protected override void OnExit()
        { }

        public override void OnLook(Vector2 mouseInput)
        {
            float xA = Character.DataTags.GetTag<CameraDataTag>().XAngle - mouseInput.y * UserSettings.XLookSensitivity;
            Character.DataTags.GetTag<CameraDataTag>().XAngle = Mathf.Clamp(xA, minXAngle, maxXAngle);
            Character.DataTags.GetTag<CameraDataTag>().YAngle += mouseInput.x * UserSettings.YLookSensitivity;
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
