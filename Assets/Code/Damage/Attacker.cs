using System.Linq;
using Code.Data;
using Code.Stats;
using UnityEngine;
using Zenject;

namespace Code
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
            if (localConfig != null) _damage = localConfig.Damage;

            TryGetComponent(out _deSpawner);
        }


        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.TryGetComponent(out ITakingDamage health))
            {
                health.TakeDamage(_damage,_owner);
                
                if (_deSpawner != null && isDestroyingAfterAttack)
                    _deSpawner.DeSpawn();
            }
        }
 
    }
}