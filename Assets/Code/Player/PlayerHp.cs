using System;
using Code.Data;
using Code.Stats;
using UnityEngine;
using Zenject;

namespace Code.Player
{
    public class PlayerHp : MonoBehaviour, IPlayerHealth
    {
        private int _currentHP;
        private int _maxHP;
        public int Current => _currentHP;
        public int Max => _maxHP;
        
        public event Action OnStatChanged;

        [Inject]
        private void Construct(GameConfig config)
        {
            _maxHP = config.playerMaxHP;
            _currentHP = _maxHP;
        }

        public void TakeDamage(int damage, DamageOwnerType damageOwner)
        {
            if (Current <= 0)
                return;
            _currentHP--;
            OnStatChanged?.Invoke();
        }
    }
}