using Code.Data;
using Code.Infrastructure.Factory;
using Code.Interfaces;
using UnityEngine;
using Zenject;

namespace Code.Bullets
{
    public class PlayerBullet : Bullet, IDetectable, IDeSpawner
    {
        private BulletsFactory _factory;
        private DiContainer _container;
        private Collider2D _collider;
        
        [Inject]
        public void Construct(DiContainer container, GameConfig config)
        {
            _container = container;
            speed = config.playerBulletSpeed;
        }

        public void OnDetect()
        {
            DeSpawn();
        }

        public void DeSpawn()
        {
            EnabledCollider(false);
            _factory = _container.Resolve<BulletsFactory>();
            _factory.DeSpawnPlayerBullet(this);
        }

        private void EnabledCollider(bool isActive) => 
            _collider.enabled = isActive;

        private void SetCollider() => 
            _collider = GetComponent<Collider2D>();

        public class Pool : MonoMemoryPool<Vector3, Vector3, PlayerBullet>
        {
            protected override void OnCreated(PlayerBullet bullet)
            {
                bullet.SetCollider();
                base.OnCreated(bullet);
            }

            protected override void Reinitialize(Vector3 position, Vector3 forward, PlayerBullet bullet)
            {
                bullet.EnabledCollider(true);
                bullet.SpawnBullet(position, forward);
            }
        }
    }
}