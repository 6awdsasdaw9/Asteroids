using System;
using System.Collections.Generic;
using Code.Data;
using ModestTree;
using UniRx;
using UniRx.Triggers;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Code
{
    public class BulletPool : MonoMemoryPool<Vector2, float, Bullet>
    {
        private readonly Bullet.Pool _bulletPool;
        private readonly List<Bullet> _bullets = new();
        

        public BulletPool(Bullet.Pool bulletPool, GameSettings settings)
        {
            _bulletPool = bulletPool;
        }


        public void Tick()
        {
            /*currentTime -= Time.deltaTime;
            if (currentTime < 0)
            {
                AddItem();
                currentTime = _spawnDelay;
            }*/
        }
    }
}