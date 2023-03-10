using Code.Stats;
using UnityEngine;

namespace Code.Enemy.SmallAsteroids
{
    public class SmallAsteroidHp : MonoBehaviour, ITakingDamage
    {
        private IDespawer _despawer;

        private void Awake()
        {
            _despawer = GetComponent<IDespawer>();
        }

        public void TakeDamage()
        {
            _despawer.Despawn();
        }
    }
}