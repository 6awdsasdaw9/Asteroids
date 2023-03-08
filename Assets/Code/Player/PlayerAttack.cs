using System;
using UnityEngine;
using Zenject;

namespace Code.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        [Inject] private Bullet.Pool _bulletPool;


        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Fire();
            }
        }

        public void Fire()
        {
            _bulletPool.Spawn(transform.position, transform.up);
        }
        
    }
}