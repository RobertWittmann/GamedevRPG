using UnityEngine;
namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float healthPoints = 100f;
        private Animator animator;
        private bool isDead = false;
        public bool IsDead { get { return isDead; } }

        private void Awake()
        {
            animator = GetComponent<Animator>();
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
        }
    }
}