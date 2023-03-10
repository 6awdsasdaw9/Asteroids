using UnityEngine;
using Zenject;

namespace Code.Enemy
{
    public class BulletsFabric
    {
        private readonly Bullet.Pool _playerBulletPool;
        private readonly Bullet.Pool _playerSuperBulletPool;
        private readonly Bullet.Pool _aliensBulletPool;

        public BulletsFabric(
            [Inject(Id = Constants.PlayerBullet)] Bullet.Pool playerBulletPool,
            [Inject(Id = Constants.PlayerSuperBullet)] Bullet.Pool playerSuperBulletPool,
            [Inject(Id = Constants.AliensBullet)] Bullet.Pool aliensBulletPool)
        {
            _playerBulletPool = playerBulletPool;
            _playerSuperBulletPool = playerSuperBulletPool;
            _aliensBulletPool = aliensBulletPool;
        }

        public void SpawnPlayerBullet(Vector3 position,Vector2 forward)
        {
            _playerBulletPool.Spawn(position, forward);
        }
        public void SpawnPlayerSuperBullet(Vector3 position,Vector2 forward)
        {
            _playerSuperBulletPool.Spawn(position, forward);
        }
        
        public void SpawnAliensBullet(Vector3 position,Vector3 forward)
        {
           Bullet bullet = _aliensBulletPool.Spawn(position, forward);
           bullet.MoveTo(forward);
        }

   
    }
}