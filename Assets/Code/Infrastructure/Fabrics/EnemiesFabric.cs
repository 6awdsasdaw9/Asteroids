using Code.Data;
using Code.Enemy.BigAsteroids;
using Code.Enemy.SmallAsteroids;
using UnityEngine;
using Zenject;

namespace Code.Enemy
{
    public class EnemiesFabric : ITickable
    {
        private readonly BigAsteroid.Pool _bigAsteroidPool;
        private readonly SmallAsteroid.Pool _smallAsteroidPool;
        
        private readonly float _asteroidsSpawnCooldown;
        private float _currentCooldown;
        
        private readonly byte _createSmallAsteroid;


        private EnemiesFabric(
            BigAsteroid.Pool bigAsteroidPool,
            SmallAsteroid.Pool smallAsteroidPool,
            GameConfig config) 
        {
            _smallAsteroidPool = smallAsteroidPool;
            _bigAsteroidPool = bigAsteroidPool;
            _asteroidsSpawnCooldown = config.asteroidsSpawnCooldown;
            _createSmallAsteroid = config.createSmallAsteroid;
            _currentCooldown = _asteroidsSpawnCooldown;
        }

        public void Tick()
        {
            if (CooldownIsUp())
            {
                CreateBigAsteroid();
                _currentCooldown = _asteroidsSpawnCooldown;
            }
            else
                UpdateCooldown();
        }

        private void CreateBigAsteroid()
        {
            var enemy = _bigAsteroidPool.Spawn();
            enemy.hp.SetActionOnDeath(CreateSmallAsteroids);
        }

        private void CreateSmallAsteroids(Transform bigAsteroid)
        {
            for (byte i = 0; i < _createSmallAsteroid; i++)
            {
                _smallAsteroidPool.Spawn(bigAsteroid.position);
            }
        }

        private void UpdateCooldown() =>
            _currentCooldown -= Time.deltaTime;

        private bool CooldownIsUp() =>
            _currentCooldown <= 0;
    }
}