using Code.Data;
using Code.Enemy;
using Code.Enemy.BigAsteroids;
using Code.Enemy.SmallAsteroids;
using Code.Player;
using Code.Services;
using Code.UI;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class LevelInstaller: MonoInstaller<LevelInstaller>
    {
        private GameSettings _settings;
        private GamePrefabs _prefabs;
        private GameConfig _config;

        [Inject]
        private void Construct(GameSettings settings, GamePrefabs prefabs, GameConfig config)
        {
            _settings = settings;
            _prefabs = prefabs;
            _config = config;
        }

        public override void InstallBindings()
        {
            BindInputController();
            BindBulletPool();
            BindHUD();
            BindPlayer();
            BindEnemy();
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

        private void BindEnemy()
        {
            Container.BindInterfacesTo<EnemiesFabric>().AsSingle().WithArguments(_config).NonLazy();
            Container.BindMemoryPool<BigAsteroid, BigAsteroid.Pool>()
                .WithInitialSize(_settings.bigAsteroidsPoolSize)
                .ExpandByOneAtATime()
                .FromComponentInNewPrefab(_prefabs.bigAsteroid)
                .UnderTransformGroup("Big Asteroids");
            
            Container.BindMemoryPool<SmallAsteroid, SmallAsteroid.Pool>()
                .WithInitialSize(_settings.smallAsteroidsPoolSize)
                .ExpandByOneAtATime()
                .FromComponentInNewPrefab(_prefabs.smallAsteroid)
                .UnderTransformGroup("Small Asteroids");
        }

        private void BindBulletPool() =>
            Container.BindMemoryPool<Bullet, Bullet.Pool>()
                .WithInitialSize(_settings.playerBulletPoolSize)
                .ExpandByOneAtATime()
                .FromComponentInNewPrefab(_prefabs.playerBullet)
                .UnderTransformGroup("Player Bullets");
    }
}