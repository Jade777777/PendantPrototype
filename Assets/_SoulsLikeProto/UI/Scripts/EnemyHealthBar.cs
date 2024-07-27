using UnityEngine;
using UnityEngine.UI;
using Mosaic;

namespace SoulsLike
{
    public class EnemyHealthBar : MonoBehaviour
    {
        private Slider slider;
        [SerializeField] Core enemyCore;
        private float maxHealth;

        void Start()
        {
            slider = GetComponent<Slider>();
            maxHealth = enemyCore.DataTags.GetTag<CombatDataTag>().Health;
            slider.value = Mathf.Clamp(enemyCore.DataTags.GetTag<CombatDataTag>().Health / maxHealth, 0, 1);
        }

        void Update()
        {
            // Update health value
            float currentHealth = Mathf.Clamp(enemyCore.DataTags.GetTag<CombatDataTag>().Health / maxHealth, 0, 1);
            slider.value = currentHealth;

            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }

            // Update position
            transform.parent.position = enemyCore.StateMachine.GetCurrentStateInstance().transform.position + new Vector3(0, 1, 0);
        }
    }
}