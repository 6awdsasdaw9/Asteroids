using System;
using Code.Data;
using Code.Stats;
using UnityEngine;
using Zenject;

namespace Code.Player
{
    public class PlayerHealth : MonoBehaviour, IHealth
    {
        private byte _currentHP;
        private byte _maxHP;
        public event Action OnStatChanged;
        public byte Current => _currentHP;
        public byte Max => _maxHP;

        [Inject]
        private void Construct(GameConfig config)
        {
            _maxHP = config.playerMaxHP;
            _currentHP = _maxHP;
        }

        public void TakeDamage()
        {
            if (Current <= 0)
                return;
            _currentHP--;
            OnStatChanged?.Invoke();

        }
    }
}