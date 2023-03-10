using Code.Data;
using Code.Stats;
using UnityEngine;
using Zenject;

namespace Code
{
    public class Attacker : MonoBehaviour
    {
        [SerializeField] private DamageType _damageType;
        [SerializeField] private bool isDestroyingAfterAttack;
        
        private IDespawer _despawer;
        private byte _damage;

        [Inject]
        private void Construct(GameConfig config)
        {
            _damage = _damageType switch
            {
                DamageType.min => config.minDamage,
                DamageType.mid => config.midDamage,
                DamageType.max => config.maxDamage,
                _ => config.minDamage
            };

            TryGetComponent(out _despawer);
        }


        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.TryGetComponent(out ITakingDamage health))
            {
                health.TakeDamage(_damage);
                
                if (_despawer != null && isDestroyingAfterAttack)
                    _despawer.Despawn();
            }
        }
        
        private enum DamageType
        {
            min,
            mid,
            max
        }
    }
}