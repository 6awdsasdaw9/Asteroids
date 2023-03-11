using System;
using Code.Data;
using Code.Enemy.Aliens;
using Code.Enemy.BigAsteroids;
using Code.Enemy.SmallAsteroids;
using UniRx;
using UnityEngine;
using Zenject;

namespace Code.Enemy
{
    public class EnemiesFactory : ITickable
    {
        private BigAsteroid.Pool _bigAsteroidPool;
        private SmallAsteroid.Pool _smallAsteroidPool;
        private Alien.Pool _alienPool;
        
        private float _asteroidsSpawnCooldown;
        private float _currentAsteroidsCooldown;
        
        private float _aliensSpawnCooldown;
        private float _currentAliensCooldown;
        
        private readonly int _createSmallAsteroid;

        private IObservable<Unit> _updateObservable;
        public float CurrentAliensCooldown => _currentAliensCooldown;
        
        private EnemiesFactory(BigAsteroid.Pool bigAsteroidPool,
            SmallAsteroid.Pool smallAsteroidPool,
            Alien.Pool alienPool,
            GameConfig config) 
        {
            _bigAsteroidPool = bigAsteroidPool;
            _smallAsteroidPool = smallAsteroidPool;
            _alienPool = alienPool;
            
            _asteroidsSpawnCooldown = config.asteroidsSpawnCooldown;
            _currentAsteroidsCooldown = _asteroidsSpawnCooldown;

            _aliensSpawnCooldown = config.aliensSpawnCooldown;
            _currentAliensCooldown = _aliensSpawnCooldown;
            
            _createSmallAsteroid = config.createSmallAsteroid;
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
            _alienPool.Spawn();
        }

        public void DeSpawnAlien(Alien alien)
        {
            _alienPool.Despawn(alien);
        }
    }
}