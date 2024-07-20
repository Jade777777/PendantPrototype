using Mosaic;
using UnityEngine;
namespace SoulsLike
{
    public abstract class BaseSoulState : BehaviorInstance
    {
        [SerializeField]
        protected float _speed = 5;

        [Header("Camera Settings")]
        [SerializeField]
        protected float cameraDistance = 1f;
        protected float minXAngle = -25;
        protected float maxXAngle = 70;

        [SerializeField]
        BehaviorInputType _death;
        protected bool IsPlayer;


        protected override void OnEnter()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public virtual void OnMove(Vector2 Input)
        {
            DataTags.GetTag<MovementDataTag>().Direction = Input;
        }
        public virtual void SetPlayerStatus(bool isPlayer)
        {
            IsPlayer = isPlayer;
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
            float xA = Character.DataTags.GetTag<CameraDataTag>().XAngle - mouseInput.y * UserSettings.XLookSensitivity;
            Character.DataTags.GetTag<CameraDataTag>().XAngle = Mathf.Clamp(xA, minXAngle, maxXAngle);
            Character.DataTags.GetTag<CameraDataTag>().YAngle += mouseInput.x * UserSettings.YLookSensitivity;
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

        public virtual void TakeDamage()
        {
            DataTags.GetTag<CombatDataTag>().Health -= DataTags.GetTag<CombatDataTag>().WeaponDamage;
            Debug.Log("Health: " + DataTags.GetTag<CombatDataTag>().Health);
            if(DataTags.GetTag<CombatDataTag>().Health <= 0)
            {

                Debug.Log("Dead!");
                Character.StateMachine.Transition(_death);
            }
            
        }

        protected void UpdatePosition()
        {
            Vector2 _moveInput = DataTags.GetTag<MovementDataTag>().Direction;
            //While rotation and camera movement happen over time, movement seems to be linear
            Vector3 input;
            //While rotation and camera movement happen over time, movement seems to be linear
            if (IsPlayer)
            {
                input = (Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0) * new Vector3(_moveInput.x, 0, _moveInput.y));
            }
            else
            {
                input = new Vector3(_moveInput.x, 0, _moveInput.y);
            }
            Vector3 Velocity = input * _speed;
            GetComponent<CharacterController>().SimpleMove(Velocity);
            Character.DataTags.GetTag<MovementDataTag>().Velocity = Velocity;
        }
        protected void UpdateRotation()//I suspect this is often driven by the animations themself. There is a distint 180 degree turn animation for switching directions
        {
            Vector2 _moveInput = DataTags.GetTag<MovementDataTag>().Direction;
            //Rotation updates extremely fast in souls games, but it is not instant, need an adjustable parameter.
            Vector3 input;
            if (IsPlayer)
            {
                input = (Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0) * new Vector3(_moveInput.x, 0, _moveInput.y));
            }
            else
            {
                input = new Vector3(_moveInput.x, 0, _moveInput.y);
            }
            float rotateSpeed = 720f * 1.5f;
            if (input.magnitude != 0f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(input, Vector3.up), rotateSpeed * Time.deltaTime);
            }
        }

        protected void UpdateCamera()
        {
            float cameraHeight = 1.5f;
            float xA = Character.DataTags.GetTag<CameraDataTag>().XAngle;

            float yA = Character.DataTags.GetTag<CameraDataTag>().YAngle;
            Camera.main.transform.position = transform.position + (Quaternion.Euler(xA, yA, 0) * Vector3.back * cameraDistance) + Vector3.up * cameraHeight;
            Camera.main.transform.rotation = Quaternion.Euler(xA, yA, 0);
        }

    }
}