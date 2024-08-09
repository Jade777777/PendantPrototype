using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mosaic;

namespace SoulsLike
{


    [CreateAssetMenu(fileName = "BackStep", menuName = "Soul/Decisions/BackStep", order = 1)]
    public class DecideBackStep : BaseDecisionAlgorithm
    {
        public override float CheckDecesion(ICore character)
        {
            if (character.DataTags.GetTag<MovementDataTag>().Direction == Vector3.zero)
            {
                Debug.Log("Do a Backstep!");
                return 1;
            }
            else
            {
                Debug.Log("NO BACKSTEP");

                return 0;
            }
        }


    }
}