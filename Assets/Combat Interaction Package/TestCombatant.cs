using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCombatant : Combatant
{
    public override void OnCombatBoxHit(CombatBox other)
    {
        switch (other)
        {
            default:
                Debug.Log("Struck by unknown combat box type");
                break;
        }
    }
}
