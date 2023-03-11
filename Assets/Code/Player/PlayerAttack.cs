using Code.Data;
using Code.Enemy;
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
        private float _superCoolDown, _currentSuperCooldown; 

        [Inject]
        private void Construct(InputController inputController, BulletsFactory bulletsFactory, GameConfig config)
        {
            _inputController = inputController;
            _bulletsFactory = bulletsFactory;
            
            _cooldown = config.playerBulletCooldown;
            _superCoolDown = config.playerSuperBulletCooldown;
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
                .Subscribe(_ => UpdateCooldown(ref _currentCooldown))
                .AddTo(this);    
            
            this.UpdateAsObservable()
                .Where(_ => !CooldownIsUp(_currentSuperCooldown))
                .Subscribe(_ => UpdateCooldown(ref _currentSuperCooldown))
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
            _currentSuperCooldown = _superCoolDown;
        }

        private void UpdateCooldown(ref float time) =>
            time -= Time.deltaTime;

        private bool CooldownIsUp(float time) =>
            time <= 0;
    }
}
