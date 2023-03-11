using Code.Data;
using Code.Enemy.Aliens;
using Code.Enemy.BigAsteroids;
using Code.Enemy.SmallAsteroids;
using Code.Player;
using Code.Stats;
using UnityEngine;
using Zenject;

namespace Code.Enemy
{
    public class EnemiesFactory : ITickable
    {
        private readonly BigAsteroid.Pool _bigAsteroidPool;
        private readonly SmallAsteroid.Pool _smallAsteroidPool;
        private readonly Alien.Pool _aliensPool;
        
        private readonly float _asteroidsSpawnCooldown;
        private float _currentAsteroidsCooldown;
        private readonly int _createSmallAsteroid;
        
        private float _aliensSpawnCooldown;
        private float _currentAliensCooldown;

        private readonly PlayerFuel _fuel;
        private readonly float _fuelFromAliens;

        public float CurrentAliensCooldown => _currentAliensCooldown;
        
        private EnemiesFactory(BigAsteroid.Pool bigAsteroidPool,
            SmallAsteroid.Pool smallAsteroidPool,
            Alien.Pool aliensPool,
            GameConfig config,
            PlayerMove player) 
        {
            _bigAsteroidPool = bigAsteroidPool;
            _smallAsteroidPool = smallAsteroidPool;
            _aliensPool = aliensPool;
            
            _asteroidsSpawnCooldown = config.asteroidsSpawnCooldown;
            _currentAsteroidsCooldown = _asteroidsSpawnCooldown;
            _createSmallAsteroid = config.createSmallAsteroid;

            _aliensSpawnCooldown = config.aliensSpawnCooldown;
            _currentAliensCooldown = _aliensSpawnCooldown;

            _fuel = player.GetComponent<PlayerFuel>();
            _fuelFromAliens = config.fuelFromEnemy;
        }

        public void Tick()
        {
            AsteroidsSpawnCycle();
            AliensSpawnCycle();
        }
        

        private void AsteroidsSpawnCycle()
        {
            if (CooldownIsUp(_currentAsteroidsCooldown))
            {
                SpawnBigAsteroid();
                _currentAsteroidsCooldown = _asteroidsSpawnCooldown;
            }
            else
                UpdateCooldown(ref _currentAsteroidsCooldown);
        }

        private void AliensSpawnCycle()
        {
            if (CooldownIsUp(_currentAliensCooldown))
            {
                SpawnAliens();
                _aliensSpawnCooldown--;
                _currentAliensCooldown = _aliensSpawnCooldown;
            }
            else
                UpdateCooldown(ref _currentAliensCooldown);
        }
        
        
        private void UpdateCooldown(ref  float time) =>
            time -= Time.deltaTime;

        private bool CooldownIsUp(float time) =>
            time <= 0;

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
            _aliensPool.Spawn();
        }

        public void DeSpawnAlien(Alien alien)
        {
            _aliensPool.Despawn(alien);
            _fuel.Replenish(_fuelFromAliens);
        }
    }
}