using Mosaic;
using SoulsLike;
using System;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    [SerializeField]
    ModifierDecorator _invinicibility;
    Guid _invincibilityID;
    ICore _core;
    private void Start()
    {
        _core = GetComponentInParent<BehaviorInstance>().Core;
    }
    public void BeginInvincibility()
    {
        Debug.Log(_core.Modifiers);
        _invincibilityID = _core.Modifiers.AddModifierDecorator(_invinicibility);
        Debug.Log("Adding Invincibility " + _invincibilityID);
    }
    public void EndInvincibility()
    {
        _core.Modifiers.RemoveModifierDecorator(_invincibilityID);
        Debug.Log("Removing invincibility" + _invincibilityID);
    }
    private void OnDisable()
    {
        _core.Modifiers.RemoveModifierDecorator(_invincibilityID);
    }
}
