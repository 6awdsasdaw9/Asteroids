using System;
using Code.Data;
using UnityEngine;
using Zenject;

namespace Code.Stats
{
    public class PlayerFuel : MonoBehaviour
    {
        private float _current;
        private float _max;
        private float _fuelForMove;

        public  Action OnStatChanged;
    
        public float Current => _current;
        public float Max => _max;

        public bool IsEnoughFuel => _current >= _fuelForMove;
    
        [Inject]
        private void Construct(GameConfig config)
        {
            _max = config.maxFuel;
            _fuelForMove = config.fuelForMove;
            _current = _max;
        }
    
        public void Reduce()
        {
            if (_current <= 0)
                return;
            
            _current -= _fuelForMove;
            OnStatChanged?.Invoke();
        }

        public void Replenish(float value)
        {
            _current += value;
        
            if (_current >= _max)
                _current = _max;
        
            OnStatChanged?.Invoke();
        }
    }
}
