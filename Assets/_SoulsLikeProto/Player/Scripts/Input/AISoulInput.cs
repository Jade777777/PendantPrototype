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
        float attackRate = 1f;
        [SerializeField]
        float maxDistance = 10f;
        float attackTime;

        private void Update()
        {

            float minDistance= 1.5f;
            float attackRange = 2f;

            Vector3 currentPosition = stateInstance.transform.position;
            Vector3 targetPosition = GetClosestEnemy().StateMachine.GetCurrentStateInstance().transform.position;
            Vector3 direction = targetPosition - currentPosition;
            float distance = direction.magnitude;
            direction.Normalize();
            if (distance > minDistance && distance < maxDistance && Time.time > attackTime + attackRate-0.5f)
            {
                stateInstance.OnMove(direction);

            }
            else if (distance < maxDistance && Time.time < attackTime + attackRate-0.5f)
            {
                stateInstance.OnMove(-direction * 0.2f);
            }
            else
            {
                stateInstance.OnMove(direction*0.01f);
            }

            if (distance< attackRange && Time.time >attackTime + attackRate)
            {
                attackTime = Time.time;
                stateInstance.OnMove(direction);
                stateInstance.OnStandardAttack();
            }
        }


        private Core GetClosestEnemy()
        {
            return _enemies[0];
        }
    }
}