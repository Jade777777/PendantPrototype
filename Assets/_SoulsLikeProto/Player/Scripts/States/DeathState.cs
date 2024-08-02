using Mosaic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;

namespace SoulsLike
{
    public class DeathState : BaseSoulState
    {
        public override void OnPause()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        protected override void OnExit()
        {

        }
    }
}
