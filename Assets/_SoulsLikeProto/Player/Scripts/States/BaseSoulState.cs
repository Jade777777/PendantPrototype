using Mosaic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
namespace SoulsLike
{
    public abstract class BaseSoulState : BehaviorInstance
    {
        
        public virtual void OnMove(Vector2 Input)
        {
            Debug.Log(this + " does not use OnMove input.");
        }
        public virtual void OnDodge()
        {
            Debug.Log(this + " does not use OnDodge input.");
        }
        public virtual void OnDash(bool IsActivated)
        {
            Debug.Log(this + " does not use OnDash input.");
        }
        public virtual void OnLook(Vector2 mouseInput)
        {
            Debug.Log(this + " does not use OnLook input.");
        }

        public virtual void OnLockOn()
        {
            Debug.Log(this + " does not use OnLockOn input.");
        }
        public virtual void OnStandardAttack()
        {
            Debug.Log(this + " does not use OnStandardAttack input.");
        }
        public virtual void OnHeavyAttack()
        {
            Debug.Log(this + " does not use OnHeavyAttack input.");
        }
        public virtual void OnGuard(bool isActivated)
        {
            Debug.Log(this + " does not use OnGuard input.");
        }
        public virtual void OnGuardCounter()
        {
            Debug.Log(this + " does not use OnGuardCounter input.");
        }
        public virtual void OnUseItem()
        {
            Debug.Log(this + " does not use OnUseItem input.");
        }
        public virtual void OnInteract()
        {
            Debug.Log(this + " does not use OnInteract input.");
        }


        //protected void UpdatePosition()
        //{
        //    //While rotation and camera movement happen over time, movement seems to be linear
        //    Vector3 relativeInput = (Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0) * new Vector3(_moveInput.x, 0, _moveInput.y));
        //    Vector3 Velocity = relativeInput * _speed;
        //    GetComponent<CharacterController>().SimpleMove(Velocity);
        //    _character.DataTagHandler.GetTag<MovementDataTag>().Velocity = Velocity;
        //}
        //protected void UpdateRotation()//I suspect this is often driven by the animations themself. There is a distint 180 degree turn animation for switching directions
        //{
        //    //Rotation updates extremely fast in souls games, but it is not instant, need an adjustable parameter.
        //    Vector3 relativeInput = (Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0) * new Vector3(_moveInput.x, 0, _moveInput.y));
        //    float rotateSpeed = 720f * 2;
        //    if (relativeInput.magnitude != 0f)
        //    {
        //        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(relativeInput, Vector3.up), rotateSpeed * Time.deltaTime);
        //    }
        //}

        //protected void UpdateAnimator()
        //{
        //    Animator animator = GetComponentInChildren<Animator>();
        //    animator.SetFloat("Velocity", _character.DataTagHandler.GetTag<MovementDataTag>().Velocity.magnitude);

        //}
        //protected void UpdateCamera()
        //{
        //    float cameraHeight = 1.5f;
        //    float xA = _character.DataTagHandler.GetTag<CameraDataTag>().XAngle;

        //    float yA = _character.DataTagHandler.GetTag<CameraDataTag>().YAngle;
        //    Camera.main.transform.position = transform.position + (Quaternion.Euler(xA, yA, 0) * Vector3.back * cameraDistance) + Vector3.up * cameraHeight;
        //    Camera.main.transform.rotation = Quaternion.Euler(xA, yA, 0);
        //}

    }
}