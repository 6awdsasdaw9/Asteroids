using Code.Stats;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    private IDespawer _despawer;
    private void Start() => 
        TryGetComponent(out _despawer);

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.TryGetComponent(out IHealth health))
        {
            Debug.Log("In Collision attacker");
            health.TakeDamage();

            if(_despawer != null)
                _despawer.Despawn();
        }
    }
}