
using UnityEngine;
using Mosaic;
namespace SoulsLike
{
    public class HealState : BaseSoulState
    {
       
        [SerializeField]
        float _attackTime = 0.5f;
        [SerializeField]
        Modifier _modifier;
        [SerializeField] 
        BehaviorInputType _nextInput;

        protected override void OnEnter()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Invoke("EndDash", _attackTime);
        }
        private void EndDash()
        {
            Core.StateMachine.Transition(_nextInput);
        }
        protected override void OnExit()
        {
            if (DataTags.GetTag<CombatDataTag>().HealthPots >= 1)
            {
                DataTags.GetTag<CombatDataTag>().HealthPots--;
                Core.Modifiers.ApplyModifier(_modifier,Core);
            }
            else
            {
                Debug.Log("Out of health pots");
            }
        }







        
    }
}
