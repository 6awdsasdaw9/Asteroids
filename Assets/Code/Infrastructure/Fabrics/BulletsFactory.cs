using UnityEngine;

namespace Code.Enemy
{
    public class BulletsFactory
    {
        private readonly PlayerBullet.Pool _playerBulletPool;
        private readonly PlayerSuperBullet.Pool _playerSuperBulletPool;
        
        public BulletsFactory(PlayerBullet.Pool playerBulletPool, PlayerSuperBullet.Pool playerSuperBulletPool)
        {
            _playerBulletPool = playerBulletPool;
            _playerSuperBulletPool = playerSuperBulletPool;
        }

        public void SpawnPlayerBullet(Vector3 position,Vector2 forward) => 
            _playerBulletPool.Spawn(position, forward);

        public void SpawnPlayerSuperBullet(Vector3 position,Vector2 forward) => 
            _playerSuperBulletPool.Spawn(position, forward);

        public void DeSpawnPlayerBullet(PlayerBullet playerBullet) => 
            _playerBulletPool.Despawn(playerBullet);

        public void DeSpawnPlayerSuperBullet(PlayerSuperBullet playerSuperBullet) => 
            _playerSuperBulletPool.Despawn(playerSuperBullet);
    }
}