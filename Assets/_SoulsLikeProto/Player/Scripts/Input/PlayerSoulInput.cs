
using UnityEngine;
using Mosaic;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Collections.Generic;

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
        private LockOnTarget _lockOnTarget;
        float _maxLockOnDistance = 20f;

        [Header("Input")]
        [SerializeField]
        public InputAction dash;


        private void Awake()
        {
            dash.performed += OnDashStart;
            dash.canceled += OnDashEnd;
        }


        void OnEnable()
        {
            dash.Enable();

        }

        void OnDisable()
        {
            dash.Disable();

        }
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
        public void OnDashStart(InputAction.CallbackContext ctx)
        {

            if (stateInstance != null)
            {
                stateInstance.OnDash(true);
            }
        }
        public void OnDashEnd(InputAction.CallbackContext ctx)
        {

            if (stateInstance != null)
            {
                stateInstance.OnDash(false);
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

                List<LockOnTarget> actors = TargetSystem.GetLockOnTargets();
                LockOnTarget target = null;
                float smallestDistanceFromTarget = 10000f;

                Vector3 lockedOnPos = _lockOnTarget.transform.position;

                Vector2 screenSize = new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);

                Debug.Log(Camera.main.ScreenToWorldPoint(new Vector3(screenSize.x, screenSize.y, 4)) + "   !  " + Camera.main.ScreenToWorldPoint(new Vector2(0.5f, 0.5f) * screenSize));
                Vector3 swipeDir = Camera.main.ScreenToWorldPoint(new Vector3(0.5f * screenSize.x + mouseInput.x, 0.5f * screenSize.y + mouseInput.y, 1f)) -
                    Camera.main.ScreenToWorldPoint(new Vector3(0.5f * screenSize.x, 0.5f * screenSize.y, 1f));

                swipeDir.Normalize();
                Debug.Log(swipeDir + "  " + mouseInput);
                foreach (LockOnTarget actor in actors)
                {

                    Vector3 enemyPos = actor.transform.position;
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

                List<LockOnTarget> actors = TargetSystem.GetLockOnTargets();
                LockOnTarget target = null;
                float smallestDistanceFromTarget = 10000f;

                Vector3 lockedOnPos = _lockOnTarget.transform.position;

                Vector2 screenSize = new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);

                Debug.Log(Camera.main.ScreenToWorldPoint(new Vector3(screenSize.x, screenSize.y, 4)) + "   !  " + Camera.main.ScreenToWorldPoint(new Vector2(0.5f, 0.5f) * screenSize));
                Vector3 swipeDir = Camera.main.ScreenToWorldPoint(new Vector3(0.5f * screenSize.x + mouseInput.x, 0.5f * screenSize.y + mouseInput.y, 1f)) -
                    Camera.main.ScreenToWorldPoint(new Vector3(0.5f * screenSize.x, 0.5f * screenSize.y, 1f));

                swipeDir.Normalize();
                Debug.Log(swipeDir + "  " + mouseInput);
                foreach (LockOnTarget actor in actors)
                {

                    Vector3 enemyPos = actor.transform.position;
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
                    List<LockOnTarget> actors = TargetSystem.GetLockOnTargets();
                    LockOnTarget target = actors[0];
                    float smallestAngle = 360;
                    foreach (LockOnTarget actor in actors)
                    {
                        //TODO:Check if valid target
                       /* if (actor == stateInstance.Core)
                        {
                            Debug.Log("Skipping Self");
                            continue;
                        }*/
                        Vector3 enemyPos = actor.transform.position;
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
            Debug.Log("Standard!!!");
            if (stateInstance != null)
                stateInstance.OnStandardAttack();
        }
        public void OnHeavyAttack(InputValue input)
        {
            Debug.Log("Heavy!!!");
            if (stateInstance != null)
                stateInstance.OnHeavyAttack();
        }
        public void OnGuard(InputValue input)
        {
            
            if (stateInstance != null)
                stateInstance.OnGuard(input.isPressed);
        }

        public void OnParry(InputValue input)
        {
            if (stateInstance != null)
                stateInstance.OnParry();
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
        }


        private void UpdateCamera()
        {
            float cameraHeight = 1.5f;

            Vector3 origin;
            Vector3 offset;
           
            if (_lockOnTarget == null)
            {
                
                origin = stateInstance.transform.position + Vector3.up * cameraHeight;
                offset = (Quaternion.Euler(_xAngle, _yAngle, 0) * Vector3.back * _cameraDistance) ;

                float radius =0.3f;
                if(Physics.SphereCast(origin, radius, offset.normalized, out RaycastHit hit, offset.magnitude+ radius, LayerMask.GetMask("Default")))
                {
                    offset = Vector3.ClampMagnitude(offset, hit.distance - radius);
                }
 
                Camera.main.transform.position = origin + offset;
                Camera.main.transform.rotation = Quaternion.Euler(_xAngle, _yAngle, 0);
            }
            else
            {
                
                Vector3 direction = stateInstance.transform.position -_lockOnTarget.transform.position;
                float distance = direction.magnitude;
                direction.Normalize();
                direction = direction + Vector3.up*(1- distance/_maxLockOnDistance);
                direction.Normalize();
                Camera.main.transform.position = stateInstance.transform.position+ Vector3.up * cameraHeight + direction*_cameraDistance;
                Vector3 lookAtTarget = (_lockOnTarget.transform.position + Vector3.up * cameraHeight - Camera.main.transform.position).normalized ;
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