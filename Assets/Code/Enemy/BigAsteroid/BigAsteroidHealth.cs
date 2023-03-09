using System;
using Code.Stats;
using UnityEngine;

namespace Code.Enemy
{
    public class BigAsteroidHealth: MonoBehaviour,IHealth
    {
        public event Action OnStatChanged;
        public byte Current { get; }
        public byte Max { get; }
        public void TakeDamage()
        {
            OnStatChanged?.Invoke();
        }
    }
}