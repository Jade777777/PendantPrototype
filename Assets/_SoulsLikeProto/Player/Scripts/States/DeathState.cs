using Mosaic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SoulsLike
{
    public class DeathState : BaseSoulState
    {
        protected override void OnExit()
        {
        }

        private void LateUpdate()
        {
            if (IsPlayer)
            {
                UpdateCamera();
            }
        }

    }
}
