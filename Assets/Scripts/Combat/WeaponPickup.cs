using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapon weapon = null;
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                Fighter fighter = other.GetComponent<Fighter>();
                if (fighter == null) return;
                if (fighter.CurrentWeapon == weapon) return;
                fighter.EquipWeapon(weapon);
                Destroy(gameObject);
            }
        }
    }
}