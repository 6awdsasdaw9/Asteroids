using System;
using Code.Data;
using Code.Interfaces;
using UnityEngine;
using Zenject;

namespace Code.Enemy.Aliens
{
    public class AlienHp: MonoBehaviour, ITakingDamage
    {
        private IDeSpawner _deSpawner;

        private int _currentHP;
        private int _maxHP;

        private Action<Transform> OnDeath;


        [Inject]
        private void Construct(GameConfig config)
        {
            _maxHP = config.aliensMaxHP;
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
                OnDeath?.Invoke(transform);
            }
        }

    }
}