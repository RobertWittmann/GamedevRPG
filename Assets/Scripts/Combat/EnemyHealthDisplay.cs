using UnityEngine;
using TMPro;
using RPG.Attributes;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter player;
        TextMeshProUGUI TextMesh;

        private void Awake()
        {
            TextMesh = GetComponent<TextMeshProUGUI>();
            player = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        }

        private void Update()
        {
            Health target = player.Target;
            if (target == null)
            {
                TextMesh.text = $"%%";
            }
            else
            {
                TextMesh.text = $"{target.GetPercentage()}%";
            }
        }
    }
}