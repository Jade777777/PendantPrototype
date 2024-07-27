using Mosaic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BasicInvincibility", menuName = "Soul / Modifier / Invincibility", order = 1)]
public class InvincibilityMod : ModifierDecorator<DamageMod>
{
    public override void Begin()
    {
        Debug.Log("Damage Negated!");
    }
}
