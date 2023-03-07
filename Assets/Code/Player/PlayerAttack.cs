using UnityEngine;
using Zenject;

namespace Code.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        
        [Inject] private BulletPool _bulletPool; // Inject the pool
        private float _projectileSpeed;

        public void Fire()
        {
            /*var projectile = _projectilePool.Spawn(transform.position, transform.rotation);
            projectile.GetComponent<Rigidbody2D>().velocity = transform.up * _projectileSpeed;*/
        }
    }
}