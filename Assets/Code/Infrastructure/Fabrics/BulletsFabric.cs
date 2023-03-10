using Code.Services;
using UnityEngine;
using Zenject;

namespace Code.Enemy
{
    public class BulletsFabric
    {
        private Bullet.Pool _bulletPool;

        [Inject]
        private void Construct(Bullet.Pool bulletPool)
        {
            _bulletPool = bulletPool;
        }

        public void SpawnBullet(Vector3 position,Vector2 forward)
        {
            _bulletPool.Spawn(position, forward);
        }
    }
}