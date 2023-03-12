using Code.Interfaces;
using UnityEngine;

namespace Code.Damage
{
    public class AttackerCollision : Attacker
    {
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.TryGetComponent(out ITakingDamage health))
            {
                Attack(health);
            }
        }
    }
}