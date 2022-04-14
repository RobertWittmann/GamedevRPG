using RPG.Saving;
using UnityEngine;
namespace RPG.Core
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] private float healthPoints = 100f;
        private Animator animator;
        private ActionScheduler actionScheduler;
        private bool isDead = false;
        public bool IsDead { get { return isDead; } }

        private void Awake()
        {
            animator = GetComponent<Animator>();
            actionScheduler = GetComponent<ActionScheduler>();
        }


        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints == 0 && !isDead)
            {
                Die();
            }
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