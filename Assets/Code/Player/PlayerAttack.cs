using System;
using Code.Data;
using Code.Enemy;
using Code.Infrastructure.Factory;
using Code.Services;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace Code.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        private InputController _inputController;
        private BulletsFactory _bulletsFactory;

        private float _cooldown, _currentCooldown;
        private float _superCooldown, _currentSuperCooldown;
        
        public Action<float, float> SuperCooldownChanged;

        [Inject]
        private void Construct(InputController inputController, BulletsFactory bulletsFactory, GameConfig config)
        {
            _inputController = inputController;
            _bulletsFactory = bulletsFactory;
            
            _cooldown = config.playerBulletCooldown;
            _superCooldown = config.playerSuperBulletCooldown;
        }

        private void Awake()
        {
            this.UpdateAsObservable()
                .Where(_ => _inputController.isPressAttack && CooldownIsUp(_currentCooldown))
                .Subscribe(_ => Attack())
                .AddTo(this);
            
            this.UpdateAsObservable()
                .Where(_ => _inputController.isPressSuperAttack && CooldownIsUp(_currentSuperCooldown))
                .Subscribe(_ => SuperAttack())
                .AddTo(this);
            
            this.UpdateAsObservable()
                .Where(_ => !CooldownIsUp(_currentCooldown))
                .Subscribe(_ => UpdateCooldown())
                .AddTo(this);    
            
            this.UpdateAsObservable()
                .Where(_ => !CooldownIsUp(_currentSuperCooldown))
                .Subscribe(_ => UpdateSuperCooldown())
                .AddTo(this);
            
        }

        
        
        private void Attack()
        {
            _bulletsFactory.SpawnPlayerBullet(transform.position, transform.up);
            _currentCooldown = _cooldown;
        }

        private void SuperAttack()
        {
            _bulletsFactory.SpawnPlayerSuperBullet(transform.position, transform.up);
            _currentSuperCooldown = _superCooldown;
        }

        private void UpdateCooldown() =>
            _currentCooldown -= Time.deltaTime;

        private void UpdateSuperCooldown()
        {
            _currentSuperCooldown -= Time.deltaTime;
            SuperCooldownChanged?.Invoke(_currentSuperCooldown,_superCooldown);
        }
        private bool CooldownIsUp(float time) =>
            time <= 0;
    }
}
