using System;
using Code.Stats;
using UnityEngine;

namespace Code
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