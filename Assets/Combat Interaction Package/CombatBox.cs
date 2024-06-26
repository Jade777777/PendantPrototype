using Mosaic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public abstract class CombatBox : MonoBehaviour //The combat box holds all of the important data
    {

        [SerializeField]
        private Combatant _combatant;
        public Combatant Combatant => _combatant;

        private void OnTriggerEnter(Collider other)
        {
           
            if (_combatant != null)
            {
                _combatant.OnCombatBoxHit(other.GetComponent<CombatBox>());
            }
            else
            {
                Debug.LogError(gameObject + "CombatBox Combatant is null!");

            }
        }

    }
}