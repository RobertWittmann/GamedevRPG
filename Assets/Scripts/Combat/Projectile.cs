using RPG.Attributes;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 1f;
        [SerializeField] bool isHoming = false;
        [SerializeField] GameObject hitEffect = null;
        [SerializeField] GameObject[] destroyOnHit = null;
        [SerializeField] float lifeAfterImpact = 0f;
        [SerializeField] float projectileLifetime = 5f;

        private Health target = null;
        private float damage = 0;
        private GameObject instigator = null;

        private void Start()
        {
            transform.LookAt(GetAimLocation());
            Destroy(gameObject, projectileLifetime);
        }

        void Update()
        {
            if (target == null) return;
            if (target.IsDead) isHoming = false;
            if (isHoming)
            {
                transform.LookAt(GetAimLocation());
            }
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        public void SetTarget(GameObject instigator, Health target, float damage)
        {
            this.target = target;
            this.damage = damage;
            this.instigator = instigator;
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if (targetCapsule == null)
            {
                return target.transform.position;
            }
            return target.transform.position + Vector3.up * targetCapsule.height / 2;
        }

        private void OnTriggerEnter(Collider other)
        {
            Health collisionTarget = other.GetComponent<Health>();
            if (collisionTarget == null) return;
            if (!collisionTarget.IsDead)
            {
                if (isHoming)
                {
                    if (collisionTarget != target) return;
                    TargetHit(target);
                }
                else if (collisionTarget)
                {
                    TargetHit(collisionTarget);
                }
            }
        }

        private void TargetHit(Health hitTarget)
        {
            if (hitEffect != null)
            {
                Instantiate(hitEffect, GetAimLocation(), transform.rotation);
            }
            foreach (GameObject toDestroy in destroyOnHit)
            {
                Destroy(toDestroy);
            }
            hitTarget.TakeDamage(instigator, damage);
            Destroy(gameObject, lifeAfterImpact);
        }
    }
}