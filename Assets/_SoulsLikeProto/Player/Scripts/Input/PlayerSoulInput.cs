using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mosaic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEditor.UIElements;

namespace SoulsLike
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerSoulInput : CharacterInput
    {
        [Header("Camera Settings")]
        [SerializeField]
        private float _cameraDistance = 1f;
        private float _minXAngle = -25;
        private float _maxXAngle = 70;
        private float _xAngle;
        private float _yAngle;
        private Vector3 _moveInput;
        private ICore _lockOnTarget;
        float _maxLockOnDistance = 20f;
        private void Update()
        {
            Move(_moveInput);
        }
        private void LateUpdate()
        {
            UpdateCamera();
        }
        private new BaseSoulState stateInstance { get { return base.stateInstance as BaseSoulState; } } //This line allows us to handle input however we would like
        public void OnMove(InputValue input)
        {
            _moveInput =new Vector3(input.Get<Vector2>().x, 0, input.Get<Vector2>().y);
        }
        public void Move(Vector3 input)
        {

            if (stateInstance != null)
            {
                Vector3 moveDir = (Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0) * input);
                stateInstance.OnMove(moveDir);
            }
        }
        public void OnDodge(InputValue input)
        {
            if (stateInstance != null)
                stateInstance.OnDodge(); 
        }
        public void OnDash(InputValue input)
        {

            if (stateInstance != null)
            {
                stateInstance.OnDash(input.isPressed);
            }
        }

        bool lockOnSwitched = false;
        public virtual void OnLook(InputValue input)
        {
            Vector2 mouseInput = input.Get<Vector2>();

            if (_lockOnTarget == null)
            {
                float xA = _xAngle - mouseInput.y * UserSettings.XLookSensitivity;
                _xAngle = Mathf.Clamp(xA, _minXAngle, _maxXAngle);
                _yAngle += mouseInput.x * UserSettings.YLookSensitivity;
            }


            if(mouseInput.magnitude == 0)
            {
                lockOnSwitched = false;
            }
            else if (_lockOnTarget != null && lockOnSwitched == false)
            {
                lockOnSwitched = true;
                switchLockOnTriggered = false;

                

                Vector3 position = Camera.main.transform.position;

                ICore[] actors = GetActors();
                ICore target = null;
                float smallestDistanceFromTarget = 10000f;

                Vector3 lockedOnPos = _lockOnTarget.StateMachine.GetCurrentStateInstance().transform.position;

                Vector2 screenSize = new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);

                Debug.Log(Camera.main.ScreenToWorldPoint(new Vector3(screenSize.x, screenSize.y, 4)) + "   !  " + Camera.main.ScreenToWorldPoint(new Vector2(0.5f, 0.5f) * screenSize));
                Vector3 swipeDir = Camera.main.ScreenToWorldPoint(new Vector3(0.5f * screenSize.x + mouseInput.x, 0.5f * screenSize.y + mouseInput.y, 1f)) -
                    Camera.main.ScreenToWorldPoint(new Vector3(0.5f * screenSize.x, 0.5f * screenSize.y, 1f));

                swipeDir.Normalize();
                Debug.Log(swipeDir + "  " + mouseInput);
                foreach (ICore actor in actors)
                {
                    if (actor == _lockOnTarget || actor == (ICore)stateInstance.Core)
                    {
                        Debug.Log("SKIPPED!");
                        continue;
                    }

                    Vector3 enemyPos = actor.StateMachine.GetCurrentStateInstance().transform.position;
                    Vector3 targetDir = enemyPos - lockedOnPos;
                    float angleToTarget = Vector3.Angle(swipeDir, targetDir);

                    float distanceFromTarget = Vector3.Distance(enemyPos, lockedOnPos);

                    float distanceFromPlayer = Vector3.Distance(enemyPos, position);



                    if (distanceFromTarget < smallestDistanceFromTarget && angleToTarget < 90f && distanceFromPlayer < _maxLockOnDistance)
                    {
                        smallestDistanceFromTarget = distanceFromTarget;
                        //smallestAngle = angleToTarget;
                        target = actor;
                    }
                }
                if (target != null)
                {
                    _lockOnTarget = target;
                }
            }
        }
        Vector2 _mouseLookBuildUp;
        bool switchLockOnTriggered = false;
        public void OnMouseLook(InputValue input)
        {
            Vector2 mouseInput = input.Get<Vector2>();

            if (_lockOnTarget == null)
            {
                float xA = _xAngle - mouseInput.y * UserSettings.XLookSensitivity;
                _xAngle = Mathf.Clamp(xA, _minXAngle, _maxXAngle);
                _yAngle += mouseInput.x * UserSettings.YLookSensitivity;
            }


            _mouseLookBuildUp += mouseInput;

            if (_lockOnTarget != null && _mouseLookBuildUp.magnitude > 400f)
            {
                mouseInput = _mouseLookBuildUp;
                switchLockOnTriggered = false;

                _mouseLookBuildUp = Vector2.zero;

                Vector3 position = Camera.main.transform.position;

                ICore[] actors = GetActors();
                ICore target = null;
                float smallestDistanceFromTarget = 10000f;

                Vector3 lockedOnPos = _lockOnTarget.StateMachine.GetCurrentStateInstance().transform.position;

                Vector2 screenSize = new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);

                Debug.Log(Camera.main.ScreenToWorldPoint(new Vector3(screenSize.x, screenSize.y, 4)) + "   !  " + Camera.main.ScreenToWorldPoint(new Vector2(0.5f, 0.5f) * screenSize));
                Vector3 swipeDir = Camera.main.ScreenToWorldPoint(new Vector3(0.5f * screenSize.x + mouseInput.x, 0.5f * screenSize.y + mouseInput.y, 1f)) -
                    Camera.main.ScreenToWorldPoint(new Vector3(0.5f * screenSize.x, 0.5f * screenSize.y, 1f));

                swipeDir.Normalize();
                Debug.Log(swipeDir + "  " + mouseInput);
                foreach (ICore actor in actors)
                {
                    if (actor == _lockOnTarget || actor == (ICore)stateInstance.Core)
                    {
                        Debug.Log("SKIPPED!");
                        continue;
                    }

                    Vector3 enemyPos = actor.StateMachine.GetCurrentStateInstance().transform.position;
                    Vector3 targetDir = enemyPos - lockedOnPos;
                    float angleToTarget = Vector3.Angle(swipeDir, targetDir);

                    float distanceFromTarget = Vector3.Distance(enemyPos, lockedOnPos);

                    float distanceFromPlayer = Vector3.Distance(enemyPos, position);



                    if (distanceFromTarget < smallestDistanceFromTarget && angleToTarget < 90f && distanceFromPlayer < _maxLockOnDistance)
                    {
                        smallestDistanceFromTarget = distanceFromTarget;
                        //smallestAngle = angleToTarget;
                        target = actor;
                    }
                }
                if (target != null)
                {
                    _lockOnTarget = target;
                }
            }
        }
        public void OnLockOn(InputValue input)
        {
            if (stateInstance != null)
            {
                if (_lockOnTarget == null )
                {

                    Vector3 lookAt = Camera.main.transform.forward;
                    Vector3 position = Camera.main.transform.position;
                    ICore[] actors = GetActors();
                    ICore target = actors[0];
                    float smallestAngle = 360;
                    foreach (ICore actor in actors)
                    {
                        if (actor == stateInstance.Core)
                        {
                            Debug.Log("Skipping Self");
                            continue;
                        }
                        Vector3 enemyPos = actor.StateMachine.GetCurrentStateInstance().transform.position;
                        float distance = Vector3.Distance(enemyPos, position);
                        float angle = Vector3.Angle(lookAt, enemyPos-position);
                        if (angle < smallestAngle&& distance< _maxLockOnDistance)
                        {
                            smallestAngle = angle;
                            target = actor;
                        }
                    }
                    _lockOnTarget = target;
                }
                else
                {
                    _lockOnTarget = null;
                }
            }
        }


        public void OnStandardAttack(InputValue input)
        {
            if (stateInstance != null)
                stateInstance.OnStandardAttack();
        }
        public void OnHeavyAttack(InputValue input)
        {
            if (stateInstance != null)
                stateInstance.OnHeavyAttack();
        }
        public void OnGuard(InputValue input)
        {
            if (stateInstance != null)
                stateInstance.OnGuard(input.isPressed);
        }
        public void OnGuardCounter(InputValue input)
        {
            if (stateInstance != null)
                stateInstance.OnGuardCounter();
        }
        public void OnUseItem(InputValue input)
        {
            if (stateInstance != null)
                stateInstance.OnUseItem();
        }
        public void OnInteract(InputValue input)
        {
            if (stateInstance != null)
                stateInstance.OnInteract();
        }
        public void OnPause(InputValue input)
        {
            if (stateInstance != null)
                stateInstance.OnPause();
            Application.Quit();
            Debug.Log("Exit Game");
        }


        private void UpdateCamera()
        {
            float cameraHeight = 1.5f;

            if (_lockOnTarget == null)
            {
                Camera.main.transform.position = stateInstance.transform.position + (Quaternion.Euler(_xAngle, _yAngle, 0) * Vector3.back * _cameraDistance) + Vector3.up * cameraHeight;
                Camera.main.transform.rotation = Quaternion.Euler(_xAngle, _yAngle, 0);
            }
            else
            {
                
                Vector3 direction = stateInstance.transform.position -_lockOnTarget.StateMachine.GetCurrentStateInstance().transform.position;
                float distance = direction.magnitude;
                direction.Normalize();
                direction = direction + Vector3.up*(1- distance/_maxLockOnDistance);
                direction.Normalize();
                Camera.main.transform.position = stateInstance.transform.position+ Vector3.up * cameraHeight + direction*_cameraDistance;
                Vector3 lookAtTarget = (_lockOnTarget.StateMachine.GetCurrentStateInstance().transform.position + Vector3.up * cameraHeight - Camera.main.transform.position).normalized ;
                Vector3 lookAtPlayer = (stateInstance.transform.position + Vector3.up * cameraHeight - Camera.main.transform.position).normalized;

                Camera.main.transform.rotation = Quaternion.LookRotation(lookAtTarget + lookAtPlayer * 2f);
            }
        }


        private ICore[] GetActors()
        {

            //This could be replace with collision detection, or even a robust object partitioning system later on.
            ICore[] actors = FindObjectsByType<Core>(FindObjectsSortMode.None);
            return actors;
        }
    }
}