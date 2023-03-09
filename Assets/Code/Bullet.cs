using System;
using Code.Data;
using Code.Services;
using Code.Stats;
using UnityEngine;
using Zenject;

namespace Code
{
    public class Bullet : MonoBehaviour , IDetectable
    {
        [SerializeField] private Rigidbody2D _rb;
        private float _speed;
        private Pool _pool;

        [Inject]
        private void Construct(Pool pool,GameConfig config)
        {
            _pool = pool;
            _speed = config.playerBulletSpeed;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (TryGetComponent(out IHealth hp))
            {
                hp.TakeDamage();
            }
        }

        private void SpawnBullet(Vector3 position,Vector2 forward)
        {
            transform.position = position;
            _rb.velocity = forward * _speed;
        }
        
        public void OnTriggerEnter()
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