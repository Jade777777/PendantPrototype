using Mosaic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatDataTag : DataTag
{
    public int Health = 10;
    public int MaxHealth = 10;
    public int HealthPots = 3;
    public int Stamina;
    public int WeaponDamage = 4;
    public float LastAttackBlockedTime;
    
}
