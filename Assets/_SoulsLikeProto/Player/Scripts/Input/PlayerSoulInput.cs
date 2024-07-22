using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mosaic;
using UnityEngine.InputSystem;

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
        [SerializeField]
        private List<ICore> _enemies;
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
        public virtual void OnLook(InputValue input)
        {
            Vector2 mouseInput= input.Get<Vector2>();
            float xA = _xAngle - mouseInput.y * UserSettings.XLookSensitivity;
            _xAngle = Mathf.Clamp(xA, _minXAngle, _maxXAngle);
            _yAngle += mouseInput.x * UserSettings.YLookSensitivity;
        }
        public void OnLockOn(InputValue input)
        {
            if (stateInstance != null)
            {
                if (_lockOnTarget == null)
                {
                    Vector3 lookAt = Camera.main.transform.forward;
                    Vector3 position = Camera.main.transform.position;
                    ICore target = _enemies[0];
                    float smallestAngle = 360;
                    foreach (ICore enemy in _enemies)
                    {
                        Vector3 enemyPos = enemy.transform.position;
                        float angle = Vector3.Angle(lookAt, position - enemyPos);
                        if (angle < smallestAngle)
                        {
                            smallestAngle = angle;
                            target = enemy;
                        }
                    }
                    _lockOnTarget = target;
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
            Application.Quit();
            Debug.Log("Exit Game");
        }


        private void UpdateCamera()
        {
            float cameraHeight = 1.5f;

            Camera.main.transform.position = stateInstance.transform.position + (Quaternion.Euler(_xAngle, _yAngle, 0) * Vector3.back * _cameraDistance) + Vector3.up * cameraHeight;
            Camera.main.transform.rotation = Quaternion.Euler(_xAngle, _yAngle, 0);
        }

        private List<ICore> GetEnemies()
        {
            return _enemies;
        }
    }
}