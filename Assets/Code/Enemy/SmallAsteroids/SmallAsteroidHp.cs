using Code.Data;
using Code.Interfaces;
using UnityEngine;

namespace Code.Enemy.SmallAsteroids
{
    public class SmallAsteroidHp : MonoBehaviour, ITakingDamage
    {
        private IDeSpawner _deSpawner;

        private void Awake()
        {
            _deSpawner = GetComponent<IDeSpawner>();
        }
        

        public void TakeDamage(int damage, DamageOwnerType damageOwner)
        {
            _deSpawner.DeSpawn();
        }
    }
}