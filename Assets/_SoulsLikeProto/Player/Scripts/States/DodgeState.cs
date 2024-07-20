using Mosaic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SoulsLike
{
    public class DodgeState : BaseSoulState
    {

        [SerializeField]
        float _dashTime = 0.5f;


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



        private void UpdateAnimator()
        {
            Animator animator = GetComponentInChildren<Animator>();
            animator.SetFloat("Velocity", Character.DataTags.GetTag<MovementDataTag>().Velocity.magnitude);

        }



    }
}
