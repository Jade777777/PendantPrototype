using Mosaic;
using SoulsLike;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulsLike
{


    public class AISoulInput : CharacterInput
    {
        [SerializeField]
        private List<Core> _enemies;
        [SerializeField]
        private List<Core> _friendlies;

        private new BaseSoulState stateInstance { get { return base.stateInstance as BaseSoulState; } } //This line allows us to handle input however we would like

        [SerializeField]
        float attackRate = 4f;
        float attackTime;

        private void Update()
        {
            stateInstance.SetPlayerStatus(false);
            float minDistance= 1.5f;
            float maxDistance= 20f;
            float attackRange = 2f;

            Vector3 currentPosition = stateInstance.transform.position;
            Vector3 targetPosition = GetClosestEnemie().StateMachine.GetCurrentStateInstance().transform.position;
            Vector3 direction = targetPosition - currentPosition;
            float distance = direction.magnitude;
            direction.Normalize();
            if (distance > minDistance && distance < maxDistance && Time.time > attackTime + attackRate)
            {
                stateInstance.OnMove(new Vector2(direction.x, direction.z));
                Debug.Log("Moving towards target");
            }
            else
            {
                stateInstance.OnMove( - new Vector2(direction.x, direction.z) *0.25f);
            }
            if (distance< attackRange && Time.time >attackTime + attackRate)
            {
                attackTime = Time.time;
                stateInstance.OnStandardAttack();
            }
        }


        private Core GetClosestEnemie()
        {
            return _enemies[0];
        }
        private List<Core> GetEnemies()
        {
            return _enemies;
        }
        private List<Core> GetFriendlies()
        {
            return _friendlies;
        }
    }
}