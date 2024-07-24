
using UnityEngine;
namespace SoulsLike
{
    public class HealState : BaseSoulState
    {
       
        [SerializeField]
        float _attackTime = 0.5f;
        [SerializeField]
        int _healing = 4;
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
            Character.StateMachine.Transition(_nextInput);
        }
        protected override void OnExit()
        {
            if (DataTags.GetTag<CombatDataTag>().HealthPots >= 1)
            {
                DataTags.GetTag<CombatDataTag>().Health--;
                DataTags.GetTag<CombatDataTag>().Health = 
                    Mathf.Clamp(DataTags.GetTag<CombatDataTag>().Health + _healing, 0, DataTags.GetTag<CombatDataTag>().MaxHealth);
                Debug.Log("Healed! " + DataTags.GetTag<CombatDataTag>().HealthPots + " health pots remaining");
            }
            else
            {
                Debug.Log("Out of health pots");
            }
        }







        
    }
}
