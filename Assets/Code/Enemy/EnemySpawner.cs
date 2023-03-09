using Code.Data;
using UnityEngine;
using Zenject;

namespace Code.Enemy
{
    public class EnemySpawner : ITickable
    {
        private readonly EnemyMovement.Factory _enemyFactory;
        private  float _cooldown;
        private float _currentCooldown;

        private EnemySpawner(EnemyMovement.Factory enemyFactory, GameConfig config)
        {
            _enemyFactory = enemyFactory;
            _cooldown = config.enemySpawnCooldown;
            _currentCooldown = _cooldown;
        }

        public void Tick()
        {
            if (CooldownIsUp())
            {
                var enemy = _enemyFactory.Create();
                enemy.SetPosition();
                enemy.Move();
                _currentCooldown = _cooldown;
            }
            else
                UpdateCooldown();
        }

        private void UpdateCooldown() =>
            _currentCooldown -= Time.deltaTime;

        private bool CooldownIsUp() =>
            _currentCooldown <= 0;
    }
}