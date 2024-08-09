using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TargetSystem
{
    private static List<LockOnTarget> _lockOnTargets = new List<LockOnTarget>();
    public static void AddLockOnTarget(LockOnTarget lockOnTarget)
    {
        _lockOnTargets.Add(lockOnTarget);
    }
    public static void RemoveLockOnTarget(LockOnTarget lockOnTargert)
    {
        _lockOnTargets .Remove(lockOnTargert);
    }
    public static List<LockOnTarget> GetLockOnTargets()
    {
        return _lockOnTargets;
    }
}
