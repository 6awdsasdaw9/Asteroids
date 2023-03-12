using Code.Interfaces;
using UnityEngine;

namespace Code.Damage
{
    public class AttackerTrigger : Attacker
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out ITakingDamage health))
            {
                Attack(health);
            }
        }
    }
}