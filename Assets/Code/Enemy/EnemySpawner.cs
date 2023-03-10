using Code.Data;
using Code.Enemy.SmallAsteroid;
using UnityEngine;
using Zenject;

namespace Code.Enemy
{
    public class EnemySpawner : ITickable
    {
        private readonly BigAsteroid.Pool _bigAsteroidPool;
        private readonly SmallAsteroid.SmallAsteroid.Pool _smallAsteroidPool;
        private readonly float _asteroidsSpawnCooldown;
        private readonly byte _createSmallAsteroid;
        private float _currentCooldown;


        private EnemySpawner(
            BigAsteroid.Pool bigAsteroidPool,
            SmallAsteroid.SmallAsteroid.Pool smallAsteroidPool,
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
            enemy.GetComponent<BigAsteroidHealth>().OnDeath += CreateSmallAsteroids;
        }

        private void CreateSmallAsteroids(BigAsteroid bigAsteroid)
        {
            for (byte i = 0; i < _createSmallAsteroid; i++)
            {
                _smallAsteroidPool.Spawn(bigAsteroid.transform.position);
            }
        }

        private void UpdateCooldown() =>
            _currentCooldown -= Time.deltaTime;

        private bool CooldownIsUp() =>
            _currentCooldown <= 0;
    }
}