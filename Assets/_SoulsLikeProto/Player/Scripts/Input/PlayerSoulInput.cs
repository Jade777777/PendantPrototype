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
        private new BaseSoulState stateInstance { get { return base.stateInstance as BaseSoulState; } } //This line allows us to handle input however we would like
        public void OnMove(InputValue input)
        {

            if (stateInstance != null)
                stateInstance.OnMove(input.Get<Vector2>());
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
        public void OnLook(InputValue input)
        {
            if (stateInstance != null)
                stateInstance.OnLook(input.Get<Vector2>());
        }
        public void OnLockOn(InputValue input)
        {
            if (stateInstance != null)
                stateInstance.OnLockOn();
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
    }
}