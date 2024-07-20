using Mosaic;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class Combatant : MonoBehaviour
{
    [SerializeField]
    ICore _core;

    [SerializeField]
    Modifier _modifier;

    [SerializeField]
    float stayTime = 0;


    [SerializeField]
    float exitTime = 0.1f;
    bool _colliding = false;
    [SerializeField]
    LayerMask _layerMask;

    [SerializeField]
    UnityEvent _onHit;

    private void Start()
    {
        _core = GetComponentInParent<BehaviorInstance>().Core;
        Debug.Log(GetComponentInParent<BehaviorInstance>());
    }

    private void FixedUpdate()
    {
        if(!IsStay())
        {
            _colliding = false;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        Combatant targetCombatant = other.transform.GetComponentInParent<Combatant>();
   
        if(targetCombatant == null || _colliding == true|| 1<< other.gameObject.layer != _layerMask)
        {
            return;
        }

        if (!targetCombatant.Equals(this)) 
        {
            Debug.Log("! collision" + other.gameObject.name +"  &  " +transform.parent.gameObject.name);

            _colliding = true;
            Debug.Log(transform.parent.name + ": I've been hit! Applying modifier:" + _modifier);
            targetCombatant._core.Modifiers.ApplyModifier(_modifier,_core);
            _onHit.Invoke();  
        }

       
    }

    private void OnTriggerStay(Collider other)
    {
        Combatant targetCombatant = other.transform.GetComponentInParent<Combatant>();

        if (targetCombatant == null || 1 << other.gameObject.layer != _layerMask)
        {
            return;
        }
  
        stayTime = Time.time;
    }
    private bool IsStay()
    {
        return stayTime + exitTime > Time.time;
    }

}
