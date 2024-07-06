using Mosaic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SoulsLike
{
    public class DodgeState : BaseSoulState
    {
        
        [SerializeField]
        float _speed = 10;
        [SerializeField]
        float _dashTime = 0.5f;

        [Header("Camera Settings")]
        [SerializeField]
        float cameraDistance = 1f;
        float minXAngle = -25;
        float maxXAngle = 70;
        [SerializeField] 
        BehaviorInputType _nextInput;

        protected override void OnEnter()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Invoke("EndDash", _dashTime);
            Vector2 _moveInput = DataTags.GetTag<MovementDataTag>().Direction;
            Vector3 relativeInput = (Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0) * new Vector3(_moveInput.x, 0, _moveInput.y));
            Vector3 Velocity = relativeInput * _speed;
            Character.DataTags.GetTag<MovementDataTag>().Velocity = Velocity;
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

        public override void OnDash(bool IsActivated)
        { 
        
        }

        public override void OnLook(Vector2 mouseInput)
        {
            float xA = Character.DataTags.GetTag<CameraDataTag>().XAngle - mouseInput.y * UserSettings.XLookSensitivity;
            Character.DataTags.GetTag<CameraDataTag>().XAngle = Mathf.Clamp(xA, minXAngle, maxXAngle);
            Character.DataTags.GetTag<CameraDataTag>().YAngle += mouseInput.x * UserSettings.YLookSensitivity;
        }

        private void Update()
        {
            UpdatePosition();
            UpdateRotation();
            UpdateAnimator();
        }

        private void LateUpdate()
        {
            UpdateCamera();
        }

        private void UpdatePosition()
        {
            //While rotation and camera movement happen over time, movement seems to be linear
            Vector3 velocity = Character.DataTags.GetTag<MovementDataTag>().Velocity;
            GetComponent<CharacterController>().SimpleMove(velocity);
            
        }

        private void UpdateRotation()//I suspect this is often driven by the animations themself. There is a distint 180 degree turn animation for switching directions
        {
            //Rotation updates extremely fast in souls games, but it is not instant, need an adjustable parameter.
            Vector3 velocity = Character.DataTags.GetTag<MovementDataTag>().Velocity;
            
            float rotateSpeed = 720f * 2;
            if (velocity.magnitude != 0f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(velocity, Vector3.up), rotateSpeed * Time.deltaTime);
            }
        }

        private void UpdateAnimator()
        {
            Animator animator = GetComponentInChildren<Animator>();
            animator.SetFloat("Velocity", Character.DataTags.GetTag<MovementDataTag>().Velocity.magnitude);

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