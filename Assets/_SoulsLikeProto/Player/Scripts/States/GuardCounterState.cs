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

        [Header("Camera Settings")]
        [SerializeField]
        float cameraDistance = 1f;
        float minXAngle = -25;
        float maxXAngle = 70;
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

        public override void OnMove(Vector2 Input)
        {
            DataTags.GetTag<MovementDataTag>().Direction = Input;
        }
        public override void OnGuard(bool isActivated)
        {
            _nextInput = isActivated ? _dash : _block;    
        }


        public override void OnLook(Vector2 mouseInput)
        {
            float xA = Character.DataTags.GetTag<CameraDataTag>().XAngle - mouseInput.y * UserSettings.XLookSensitivity;
            Character.DataTags.GetTag<CameraDataTag>().XAngle = Mathf.Clamp(xA, minXAngle, maxXAngle);
            Character.DataTags.GetTag<CameraDataTag>().YAngle += mouseInput.x * UserSettings.YLookSensitivity;
        }

        private void Update()
        {
        }

        private void LateUpdate()
        {
            UpdateCamera();
        }


        private void UpdateCamera()
        {
            float cameraHeight = 1.5f;
            float xA = Character.DataTags.GetTag<CameraDataTag>().XAngle;

            float yA = Character.DataTags.GetTag<CameraDataTag>().YAngle;
            Camera.main.transform.position = transform.position + (Quaternion.Euler(xA, yA, 0) * Vector3.back * cameraDistance) + Vector3.up * cameraHeight;
            Camera.main.transform.rotation = Quaternion.Euler(xA, yA, 0);
        }


    }
}
