using Mosaic;
using UnityEngine;
namespace SoulsLike
{
    public abstract class BaseSoulState : BehaviorInstance
    {
        [SerializeField]
        protected float _speed = 5;



        [SerializeField]
        BehaviorInputType _death;
        protected bool IsPlayer;


        protected override void OnEnter()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public virtual void OnMove(Vector3 Input)
        {
            DataTags.GetTag<MovementDataTag>().Direction = Input;
        }

        public virtual void OnDodge()
        {
            //Debug.Log(this + " does not use OnDodge input.");
        }
        public virtual void OnDash(bool IsActivated)
        {
           // Debug.Log(this + " does not use OnDash input.");
        }
        public virtual void OnLook(Vector2 mouseInput)
        {

        }

        public virtual void OnLockOn()
        {
            //Debug.Log(this + " does not use OnLockOn input.");
        }
        public virtual void OnStandardAttack()
        {
           // Debug.Log(this + " does not use OnStandardAttack input.");
        }
        public virtual void OnHeavyAttack()
        {
           // Debug.Log(this + " does not use OnHeavyAttack input.");
        }
        public virtual void OnGuard(bool isActivated)
        {
           // Debug.Log(this + " does not use OnGuard input.");
        }
        public virtual void OnGuardCounter()
        {
           // Debug.Log(this + " does not use OnGuardCounter input.");
        }
        public virtual void OnUseItem()
        {
           // Debug.Log(this + " does not use OnUseItem input.");
        }
        public virtual void OnInteract()
        {
           // Debug.Log(this + " does not use OnInteract input.");
        }

        public virtual void OnPause()
        {
            Debug.Log("Pause menu not implemented, quitting");
            Application.Quit();
        }
        protected void UpdatePosition()
        {
            Vector3 _moveInput = DataTags.GetTag<MovementDataTag>().Direction;
            //While rotation and camera movement happen over time, movement seems to be linear

            Vector3 Velocity = _moveInput * _speed;
            GetComponent<CharacterController>().SimpleMove(Velocity);
            Core.DataTags.GetTag<MovementDataTag>().Velocity = Velocity;
        }
        protected void UpdateRotation()//I suspect this is often driven by the animations themself. There is a distint 180 degree turn animation for switching directions
        {
            Vector3 input = DataTags.GetTag<MovementDataTag>().Direction;
            input.y = 0;
            //Rotation updates extremely fast in souls games, but it is not instant, need an adjustable parameter.

            float rotateSpeed = 720f * 1.5f;
            if (input.magnitude != 0f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(input, Vector3.up), rotateSpeed * Time.deltaTime);
            }
        }



    }
}