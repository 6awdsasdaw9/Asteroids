using System;
using Code.Stats;
using UnityEngine;

namespace Code.Enemy.SmallAsteroid
{
    public class SmallAsteroidHealth : MonoBehaviour, IHealth
    {
        private IDespawer _despawer;

        private void Start()
        {
            _despawer = GetComponent<IDespawer>();
        }


        public event Action OnStatChanged;
        public byte Current { get; }
        public byte Max { get; }

        public void TakeDamage()
        {
            _despawer.Despawn();
        }
    }
}