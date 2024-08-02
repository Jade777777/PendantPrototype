using UnityEngine;
using UnityEngine.UI;
using Mosaic;

namespace SoulsLike
{
    public class CanvasFacePlayer : MonoBehaviour
    {
        [SerializeField] Core enemyCore;
        private Canvas canvas;
        [SerializeField] float activationDistance = 9f;

        void Start()
        {
            canvas = GetComponent<Canvas>();
        }


        void Update()
        {
            // Determine positions
            Vector3 cameraPosition = Camera.main.transform.position;
            Vector3 enemyPosition = enemyCore.StateMachine.GetCurrentStateInstance().transform.position;

            // Determine distance
            float distance = Vector3.Distance(cameraPosition, enemyPosition);

            // Determine if the canvas is within the activation distance
            if (distance <= activationDistance)
            {
                canvas.enabled = true;
                Vector3 direction = cameraPosition - enemyPosition;
                direction.y = 0;
                transform.rotation = Quaternion.LookRotation(direction);
            }
            else
            {
                canvas.enabled = false;
            }


        }
    }
}
