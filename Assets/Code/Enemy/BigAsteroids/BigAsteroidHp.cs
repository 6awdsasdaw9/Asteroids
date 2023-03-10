using System;
using Code.Data;
using Code.Stats;
using UnityEngine;
using Zenject;

namespace Code.Enemy.BigAsteroids
{
    public class BigAsteroidHp : MonoBehaviour, ITakingDamage
    {
        private IDespawer _despawer;
        
        private byte _currentHP;
        private byte _maxHP;
        
        private Action<Transform> OnDeath;
        

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
            if (_currentHP <= 0)
            {
                OnDeath?.Invoke(transform);
                _despawer.Despawn();
            }
        }

        public void SetActionOnDeath(Action<Transform> action) => 
            OnDeath = action;
    }
}