using UnityEngine;
using TMPro;

namespace RPG.Attributes
{
    public class ExperienceDisplay : MonoBehaviour
    {
        Experience experience;
        TextMeshProUGUI TextMesh;

        private void Awake()
        {
            TextMesh = GetComponent<TextMeshProUGUI>();
            experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
        }

        private void Update()
        {
            TextMesh.text = $"{experience.ExperiencePoints}";
        }
    }
}