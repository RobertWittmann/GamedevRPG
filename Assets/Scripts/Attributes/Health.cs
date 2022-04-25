using System;
using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using UnityEngine;
namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float regenerationPercentage = 0.7f;

        private float healthPoints = -1f;
        public float HealthPoints { get { return healthPoints; } }
        private bool isDead = false;
        public bool IsDead { get { return isDead; } }

        private Animator animator;
        private ActionScheduler actionScheduler;
        private BaseStats baseStats;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            actionScheduler = GetComponent<ActionScheduler>();
            baseStats = GetComponent<BaseStats>();
        }

        private void Start()
        {
            if (healthPoints < 0)
            {
                healthPoints = baseStats.GetStat(Stat.Health);
            }
            baseStats.onLevelUp += RegenerateHealth;
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            Debug.Log(gameObject.name + "took damage: " + damage);

            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints == 0 && !isDead)
            {
                AwardExperience(instigator);
                Die();
            }
        }

        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) return;
            experience.GainExperience(baseStats.GetStat(Stat.ExperienceReward));

        }

        private void RegenerateHealth()
        {
            float regenHealth = baseStats.GetStat(Stat.Health) * regenerationPercentage;
            healthPoints = MathF.Max(regenHealth, healthPoints);
        }

        public float GetPercentage()
        {
            return Mathf.Ceil(100 * (healthPoints / baseStats.GetStat(Stat.Health)));
        }

        public float GetMaxHealth()
        {
            return baseStats.GetStat(Stat.Health);
        }

        private void Die()
        {
            isDead = true;
            animator.SetTrigger("die");
            actionScheduler.CancelCurrentAction();
        }

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float)state;
            if (healthPoints <= 0)
            {
                Die();
            }
        }
    }
}