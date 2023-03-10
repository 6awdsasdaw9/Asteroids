using System.Linq;
using Code.Data;
using Code.Interfaces;
using UnityEngine;
using Zenject;

namespace Code.Damage
{
    public class Attacker : MonoBehaviour
    {
        [SerializeField] private DamageOwnerType _owner;
        [SerializeField] private bool isDestroyingAfterAttack;

        private IDeSpawner _deSpawner;
        private int _damage;

        [Inject]
        private void Construct(GameConfig config)
        {
            var localConfig = config.DamageConfig.FirstOrDefault(d => d.DamageOwner == _owner);
            if (localConfig != null)
                _damage = localConfig.Damage;

            TryGetComponent(out _deSpawner);
        }

        protected void Attack(ITakingDamage health)
        {
            health.TakeDamage(_damage, _owner);

            if (_deSpawner != null && isDestroyingAfterAttack)
                _deSpawner.DeSpawn();
        }
    }
}