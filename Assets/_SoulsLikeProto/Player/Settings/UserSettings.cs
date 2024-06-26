using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SoulsLike
{

    [CreateAssetMenu(fileName = nameof(UserSettings), menuName = "Settings/" + nameof(UserSettings))]
    public static class UserSettings
    {
        public static float XLookSensitivity = 0.4f;
        public static float YLookSensitivity = 1f;
    }
}