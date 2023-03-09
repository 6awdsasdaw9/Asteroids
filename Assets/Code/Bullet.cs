using Code.Services;
using UnityEngine;
using Zenject;

namespace Code
{
    public class Bullet : MonoBehaviour , IDetectable
    {
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField, Range(1, 10)] private float _speed = 1;
        private Pool _pool;

        [Inject]
        private void Construct(Pool pool)
        {
            _pool = pool;
        }

        private void SpawnBullet(Vector3 position,Vector2 forward)
        {
            transform.position = position;
            _rb.velocity = forward * _speed;
        }
        
        public void OnEnter()
        {
            _pool.Despawn(this);
        }

        public class Pool : MonoMemoryPool<Vector3,Vector2,Bullet>
        {
            protected override void Reinitialize(Vector3 position,Vector2 forward,Bullet bullet)
            {
                bullet.SpawnBullet(position,forward);
            }
        }
    }
}