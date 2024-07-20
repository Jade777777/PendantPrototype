using UnityEngine;
using UnityEngine.UI;
using Mosaic;

namespace SoulsLike
{
    public class HealthbarUI : MonoBehaviour
    {
        private Core playerCore;
        private Slider healthSlider;
        private float maxHealth;

        void Start()
        {
            playerCore = GameObject.Find("Player").GetComponent<Core>();
            healthSlider = GetComponent<Slider>();
            maxHealth = playerCore.DataTags.GetTag<CombatDataTag>().Health;
        }

        void Update()
        {
            float currentHealth = Mathf.Clamp(playerCore.DataTags.GetTag<CombatDataTag>().Health / maxHealth, 0, 1);
            healthSlider.value = currentHealth;
        }
    }
}