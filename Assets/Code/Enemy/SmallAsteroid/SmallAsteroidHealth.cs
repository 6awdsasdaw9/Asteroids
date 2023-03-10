using System;
using Code.Data;
using Code.Stats;
using UnityEngine;
using Zenject;

namespace Code.Enemy.SmallAsteroid
{
    public class SmallAsteroidHealth : MonoBehaviour,IHealth
    {
        [SerializeField] private SmallAsteroid _asteroid;
        private byte _currentHP;
        private byte _maxHP;

        public byte Current => _currentHP;
        public byte Max => _maxHP;

        public event Action OnStatChanged;


        [Inject]
        private void Construct(GameConfig config)
        {
            _maxHP = config.smallAsteroidMaxHP;
            _currentHP = _maxHP;
        }
        
        public void TakeDamage()
        {
            _currentHP--;
            OnStatChanged?.Invoke();
            
            if (Current <= 0)
            {
                _asteroid.Despawn();
            }
        }
    }
}