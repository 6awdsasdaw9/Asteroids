using System;
using UnityEngine;
using Zenject;
namespace Code
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField,Range(1,5)] private float _speed = 1;

        private void Move(Vector2 direction)
        {
            _rb.velocity = direction * _speed;
        }
        
        
        public class Pool : MemoryPool<Bullet,Vector2>
        {
            protected override void Reinitialize(Bullet bullet, Vector2 direction)
            {
                bullet.Move(direction);
            }
        }
    }
}