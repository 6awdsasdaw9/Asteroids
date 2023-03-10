using Code.Data;
using Code.Services;
using UnityEngine;
using Zenject;

namespace Code
{
    public class Bullet : MonoBehaviour , IDetectable , IDespawer
    {
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private byte damage = 1;
        private float _speed;
        private Pool _pool;

        [Inject]
        private void Construct(Pool pool,GameConfig config)
        {
            _pool = pool;
            _speed = config.playerBulletSpeed;
        }
        
        private void SpawnBullet(Vector3 position,Vector2 forward)
        {
            transform.position = position;
            _rb.velocity = forward * _speed;
        }

        public void OnDetect() =>
            Despawn();

        public void Despawn() => 
            _pool.Despawn(this);

        public class Pool : MonoMemoryPool<Vector3,Vector2,Bullet>
        {
            protected override void Reinitialize(Vector3 position,Vector2 forward,Bullet bullet)
            {
                bullet.SpawnBullet(position,forward);
            }
        }
    }
}