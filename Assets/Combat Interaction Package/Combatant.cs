using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public abstract class Combatant : MonoBehaviour
    {
        public abstract void OnCombatBoxHit(CombatBox other);// Implement type case statement for each valid combat box
    }
}