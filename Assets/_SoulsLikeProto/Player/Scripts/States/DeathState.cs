using Mosaic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SoulsLike
{
    public class DeathState : BaseSoulState
    {


        [Header("Camera Settings")]
        [SerializeField]
        float cameraDistance = 1f;
        float minXAngle = -25;
        float maxXAngle = 70;


        protected override void OnEnter()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

        }



        public override void OnLook(Vector2 mouseInput)
        {
            float xA = Character.DataTags.GetTag<CameraDataTag>().XAngle - mouseInput.y * UserSettings.XLookSensitivity;
            Character.DataTags.GetTag<CameraDataTag>().XAngle = Mathf.Clamp(xA, minXAngle, maxXAngle);
            Character.DataTags.GetTag<CameraDataTag>().YAngle += mouseInput.x * UserSettings.YLookSensitivity;
        }


        private void LateUpdate()
        {
            if (IsPlayer)
            {
                UpdateCamera();
            }
        }


        private void UpdateCamera()
        {
            float cameraHeight = 1.5f;
            float xA = Character.DataTags.GetTag<CameraDataTag>().XAngle;

            float yA = Character.DataTags.GetTag<CameraDataTag>().YAngle;
            Camera.main.transform.position = transform.position + (Quaternion.Euler(xA, yA, 0) * Vector3.back * cameraDistance) + Vector3.up * cameraHeight;
            Camera.main.transform.rotation = Quaternion.Euler(xA, yA, 0);
        }

        protected override void OnExit()
        {

        }
    }
}
