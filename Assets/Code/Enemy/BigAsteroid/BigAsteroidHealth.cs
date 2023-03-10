using System;
using Code.Data;
using Code.Stats;
using UnityEngine;
using Zenject;

namespace Code.Enemy
{
    public class BigAsteroidHealth : MonoBehaviour, IHealth
    {
        private IDespawer _despawer;
        private byte _currentHP;
        private byte _maxHP;

        public byte Current => _currentHP;
        public byte Max => _maxHP;

        public event Action OnStatChanged;
        public Action<Transform> OnDeath;

        [Inject]
        private void Construct(GameConfig config)
        {
            _maxHP = config.bigAsteroidMaxHP;
            _despawer = GetComponent<IDespawer>();
        }

        public void ResetHealth()
        {
            _currentHP = _maxHP;
        }

        public void TakeDamage()
        {
            _currentHP--;
            Debug.Log(_currentHP);
            if (_currentHP <= 0)
            {
                OnDeath?.Invoke(transform);
                OnDeath = null;
                _despawer.Despawn();
            }
        }
    }
}