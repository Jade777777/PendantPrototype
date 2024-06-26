using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SoulsLike
{
    public class GuardState : BaseSoulState
    {
        Vector2 _moveInput => DataTags.GetTag<MovementDataTag>().Direction;
        [SerializeField]
        float _speed = 3;

        [Header("Camera Settings")]
        [SerializeField]
        float cameraDistance= 3f;
        float minXAngle = -25;
        float maxXAngle = 70;
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
            Vector3 relativeInput = (Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0) * new Vector3(_moveInput.x, 0, _moveInput.y));
            Vector3 Velocity = relativeInput * _speed;
            GetComponent<CharacterController>().SimpleMove(Velocity);
            Character.DataTags.GetTag<MovementDataTag>().Velocity = Velocity;
        }
        private void UpdateRotation()//I suspect this is often driven by the animations themself. There is a distint 180 degree turn animation for switching directions
        {
            //Rotation updates extremely fast in souls games, but it is not instant, need an adjustable parameter.
            Vector3 relativeInput = (Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0) * new Vector3(_moveInput.x, 0, _moveInput.y));
            float rotateSpeed = 720f * 2;
            if (relativeInput.magnitude != 0f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(relativeInput, Vector3.up), rotateSpeed * Time.deltaTime);
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

