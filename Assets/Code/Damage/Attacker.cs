using Code.Stats;
using UnityEngine;

namespace Code
{
    public class Attacker : MonoBehaviour
    {
        [SerializeField] private bool isDestroyingAfterAttack;
        private IDespawer _despawer;

        private void Start() =>
            TryGetComponent(out _despawer);

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.TryGetComponent(out ITakingDamage health))
            {
            
                health.TakeDamage();
                
                if (_despawer != null && isDestroyingAfterAttack)
                    _despawer.Despawn();
            }
        }
    }
}