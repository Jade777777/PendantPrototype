using Mosaic;
using SoulsLike;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SoulsLike {
    public class BackStepState : BaseSoulState
    {

        [SerializeField]
        float _dodgeTime = 0.5f;
        [SerializeField]
        float _dodgeDistance = 3f;

        [SerializeField]
        BehaviorInputType _nextInput;

        Vector3 _startingPos;
        Vector3 _direction;
        float _distance;
        protected override void OnEnter()
        {

            base.OnEnter();
            Invoke(nameof(EndDash), _dodgeTime);


            _startingPos = transform.position;
            _direction = -transform.forward;
            _distance = _dodgeDistance;
        }
        private void EndDash()
        {
            Core.StateMachine.Transition(_nextInput);
        }
        protected override void OnExit()
        { }

        public override void OnDash(bool IsActivated)
        {

        }

        private void Update()
        {
            UpdatePosition();
            UpdateRotation();
            UpdateAnimator();
        }

        protected override void UpdatePosition()
        {
            float speed = _dodgeDistance / _dodgeTime;
            Vector3 dodge = _direction * speed * Time.deltaTime;
            GetComponent<CharacterController>().Move(dodge);

        }
        private void UpdateAnimator()
        {
            Animator animator = GetComponentInChildren<Animator>();
            animator.SetFloat("Velocity", Core.DataTags.GetTag<MovementDataTag>().Velocity.magnitude);

        }
    }
}
