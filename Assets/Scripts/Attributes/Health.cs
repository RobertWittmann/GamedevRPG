using System;
using GameDevTV.Utils;
using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using UnityEngine;
namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float regenerationPercentage = 0.7f;

        private LazyValue<float> healthPoints;
        public float HealthPoints { get { return healthPoints.value; } }
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

            healthPoints = new LazyValue<float>(GetInitialHealth);
        }

        private float GetInitialHealth()
        {
            return baseStats.GetStat(Stat.Health);
        }

        private void Start()
        {
            healthPoints.ForceInit();
        }

        private void OnEnable()
        {
            baseStats.onLevelUp += RegenerateHealth;
        }

        private void OnDisable()
        {
            baseStats.onLevelUp -= RegenerateHealth;
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            Debug.Log(gameObject.name + "took damage: " + damage);

            healthPoints.value = Mathf.Max(healthPoints.value - damage, 0);
            if (healthPoints.value == 0 && !isDead)
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
            healthPoints.value = MathF.Max(regenHealth, healthPoints.value);
        }

        public float GetPercentage()
        {
            return Mathf.Ceil(100 * (healthPoints.value / baseStats.GetStat(Stat.Health)));
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
            return healthPoints.value;
        }

        public void RestoreState(object state)
        {
            healthPoints.value = (float)state;
            if (healthPoints.value <= 0)
            {
                Die();
            }
        }
    }
}