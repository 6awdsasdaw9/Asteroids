using System;
using Code.Data;
using UnityEngine;
using Zenject;

namespace Code.Stats
{
    public class PlayerHealth : MonoBehaviour, IHealth
    {
        private byte _currentHp;
        private byte _maxHp;

        public byte Current => _currentHp;
        public byte Max => _maxHp;
        
        public event Action OnStatChanged;

        [Inject]
        private void Construct(GameConfig config)
        {
            _maxHp = config.playerMaxHP;
            _currentHp = _maxHp;
        }
        
        public void TakeDamage()
        {
            if (_currentHp <= 0)
                return;
            
            _currentHp--;
            OnStatChanged?.Invoke();
        }
        
    }
}