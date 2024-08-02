using Mosaic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SoulsLike
{
    public class InteractState : BaseSoulState
    {
       
        [SerializeField]
        float _interactTime = 0.5f;

        float _range = 3f;
        [SerializeField] 
        BehaviorInputType _nextInput;

        protected override void OnEnter()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Invoke("EndDash", _interactTime);
        }
        private void EndDash()
        {
            Collider[] interactables = Physics.OverlapSphere(transform.position, _range, 1 << LayerMask.NameToLayer("Interactable"));
            if(interactables.Length > 0 )
            {
                foreach(Collider collider in interactables)
                {
                    //TODO: Implement checks for valid positioning
                }
                interactables[0].GetComponentInParent<IInteractable>().Interact();
            }
            Core.StateMachine.Transition(_nextInput);
        }
        protected override void OnExit()
        { }



    }
}
