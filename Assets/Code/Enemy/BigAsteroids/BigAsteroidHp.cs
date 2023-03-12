using System;
using Code.Data;
using Code.Interfaces;
using UnityEngine;
using Zenject;

namespace Code.Enemy.BigAsteroids
{
    public class BigAsteroidHp : MonoBehaviour, ITakingDamage
    {
        private IDeSpawner _deSpawner;

        private int _currentHP;
        private int _maxHP;

        private Action<Transform> OnDeath;


        [Inject]
        private void Construct(GameConfig config)
        {
            _maxHP = config.bigAsteroidMaxHP;
            _deSpawner = GetComponent<IDeSpawner>();
        }

        public void ResetHealth()
        {
            _currentHP = _maxHP;
        }

        public void TakeDamage(int damage, DamageOwnerType damageOwner)
        {
            _currentHP -= damage;
            if (_currentHP <= 0)
            {
                _deSpawner.DeSpawn();
                
                if (damageOwner != DamageOwnerType.PlayerSuperBullet)
                {
                    OnDeath?.Invoke(transform);
                }
            }
        }

        public void SetActionOnDeath(Action<Transform> action) =>
            OnDeath = action;
    }
}