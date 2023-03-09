using Code.Data;
using Code.Player;
using Code.Services;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class LevelInstaller: MonoInstaller<LevelInstaller>
    {
        private GameSettings _settings;
        private GamePrefabs _prefabs;

        [Inject]
        private void Construct(GameSettings settings,GamePrefabs prefabs)
        {
            _settings = settings;
            _prefabs = prefabs;
        }

        public override void InstallBindings()
        {
            BindInputController();
            BindBulletPool();
            BindHUD();
            BindPlayer();
        }
        
        private void BindInputController() => 
            Container.Bind<InputController>().AsSingle().NonLazy();

        private void BindHUD()
        {
            UIDisplay hud = Container.InstantiatePrefabForComponent<UIDisplay>(_prefabs.hud);
            Container.Bind<UIDisplay>().FromInstance(hud);
        }
        private void BindPlayer()
        {
            PlayerMove player = Container.InstantiatePrefabForComponent<PlayerMove>(_prefabs.player,Vector3.zero, Quaternion.identity, null);
            Container.Bind<PlayerMove>().FromInstance(player);
        }

        private void BindBulletPool() =>
            Container.BindMemoryPool<Bullet, Bullet.Pool>()
                .WithInitialSize(_settings.playerBulletPoolSize)
                .ExpandByOneAtATime()
                .FromComponentInNewPrefab(_prefabs.playerBullet)
                .UnderTransformGroup("Player Bullets");
    }
}