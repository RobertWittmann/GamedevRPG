using UnityEngine;
using TMPro;

namespace RPG.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {
        Health health;
        TextMeshProUGUI TextMesh;

        private void Awake()
        {
            TextMesh = GetComponent<TextMeshProUGUI>();
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
        }

        private void Update()
        {
            TextMesh.text = $"{Mathf.Ceil(health.HealthPoints)}/{health.GetMaxHealth()}";
        }
    }
}