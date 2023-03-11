using Code.Data;
using Code.Enemy.Aliens;
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
        private readonly Alien.Pool _alienPool;
        
        private readonly float _asteroidsSpawnCooldown;
        private float _currentCooldown;
        
        private readonly int _createSmallAsteroid;


        private EnemiesFabric(BigAsteroid.Pool bigAsteroidPool,
            SmallAsteroid.Pool smallAsteroidPool,
            Alien.Pool alienPool,
            GameConfig config) 
        {
            _bigAsteroidPool = bigAsteroidPool;
            _smallAsteroidPool = smallAsteroidPool;
            _alienPool = alienPool;
            
            _asteroidsSpawnCooldown = config.asteroidsSpawnCooldown;
            _createSmallAsteroid = config.createSmallAsteroid;
            _currentCooldown = _asteroidsSpawnCooldown;
        }

        public void Tick()
        {
            if (CooldownIsUp())
            {
                SpawnBigAsteroid();
                SpawnAliens();
                _currentCooldown = _asteroidsSpawnCooldown;
            }
            else
                UpdateCooldown();
        }
        
        private void UpdateCooldown() =>
            _currentCooldown -= Time.deltaTime;

        private bool CooldownIsUp() =>
            _currentCooldown <= 0;

        private void SpawnBigAsteroid()
        {
            var enemy = _bigAsteroidPool.Spawn();
            enemy.hp.SetActionOnDeath(SpawnSmallAsteroids);
        }

        private void SpawnSmallAsteroids(Transform bigAsteroid)
        {
            for (byte i = 0; i < _createSmallAsteroid; i++)
            {
                _smallAsteroidPool.Spawn(bigAsteroid.position);
            }
        }

        private void SpawnAliens()
        {
            _alienPool.Spawn();
        }

        public void DeSpawnAlien(Alien alien)
        {
            _alienPool.Despawn(alien);
        }
    }
}