using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnTarget : MonoBehaviour
{

    private void OnEnable()
    {
        TargetSystem.AddLockOnTarget(this);
    }
    private void OnDisable()
    {
        TargetSystem.RemoveLockOnTarget(this);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.1f);
    }
}
