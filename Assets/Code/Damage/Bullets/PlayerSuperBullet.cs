using Code.Data;
using Code.Enemy;
using Code.Services;
using UnityEngine;
using Zenject;

namespace Code
{
    public class PlayerSuperBullet : Bullet, IDetectable, IDeSpawner
    {
        private BulletsFactory _factory;
        private DiContainer _container;
        private Collider2D _collider;

        [Inject]
        public void Construct(DiContainer container, GameConfig config)
        {
            _container = container;
            speed = config.playerSuperBulletSpeed;
        }

        public void OnDetect()
        {
            DeSpawn();
        }

        public void DeSpawn()
        {
            EnabledCollider(false);
            _factory = _container.Resolve<BulletsFactory>();
            _factory.DeSpawnPlayerSuperBullet(this);
        }

        private void EnabledCollider(bool isActive) =>
            _collider.enabled = isActive;

        private void SetCollider() =>
            _collider = GetComponent<Collider2D>();

        public class Pool : MonoMemoryPool<Vector3, Vector3, PlayerSuperBullet>
        {
            protected override void OnCreated(PlayerSuperBullet bullet)
            {
                bullet.SetCollider();
                base.OnCreated(bullet);
            }

            protected override void Reinitialize(Vector3 position, Vector3 forward, PlayerSuperBullet bullet)
            {
                bullet.EnabledCollider(true);
                bullet.SpawnBullet(position, forward);
            }
        }
    }
}