using UnityEngine;
using TMPro;

namespace RPG.Stats
{
    public class LevelDisplay : MonoBehaviour
    {
        BaseStats baseStats;
        TextMeshProUGUI TextMesh;

        private void Awake()
        {
            TextMesh = GetComponent<TextMeshProUGUI>();
            baseStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
        }

        private void Update()
        {
            TextMesh.text = $"{baseStats.GetLevel()}";
        }
    }
}