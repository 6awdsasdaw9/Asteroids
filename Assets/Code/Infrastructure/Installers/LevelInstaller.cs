using Code.Data;
using Code.Enemy;
using Code.Enemy.Aliens;
using Code.Enemy.BigAsteroids;
using Code.Enemy.SmallAsteroids;
using Code.Player;
using Code.Services;
using Code.UI;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class LevelInstaller : MonoInstaller<LevelInstaller>
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
            BindBulletPools();
            BindHUD();
            BindPlayer();
            BindEnemiesPool();
            BindActors();
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
            PlayerMove player =
                Container.InstantiatePrefabForComponent<PlayerMove>(_prefabs.player, Vector3.zero, Quaternion.identity,
                    null);
            Container.Bind<PlayerMove>().FromInstance(player);
        }

        private void BindEnemiesPool()
        {
            Container.BindInterfacesAndSelfTo<EnemiesFactory>().AsSingle().NonLazy();

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


            Container.BindMemoryPool<Alien, Alien.Pool>()
                .WithInitialSize(_settings.aliensPoolSize)
                .ExpandByOneAtATime()
                .FromComponentInNewPrefab(_prefabs.alien)
                .UnderTransformGroup("Aliens");
        }

        private void BindBulletPools()
        {
            Container.BindInterfacesAndSelfTo<BulletsFactory>().AsSingle().NonLazy();

            Container.BindMemoryPool<PlayerBullet, PlayerBullet.Pool>()
                .WithInitialSize(_settings.playerBulletPoolSize)
                .ExpandByOneAtATime()
                .FromComponentInNewPrefab(_prefabs.playerBullet)
                .UnderTransformGroup(Constants.PlayerBullet)
                .NonLazy();

            Container.BindMemoryPool<PlayerSuperBullet, PlayerSuperBullet.Pool>()
                .WithInitialSize(_settings.playerSuperBulletPoolSize)
                .ExpandByOneAtATime()
                .FromComponentInNewPrefab(_prefabs.playerSuperBullet)
                .UnderTransformGroup(Constants.PlayerSuperBullet)
                .NonLazy();
        }

        private void BindActors()
        {
            Container.Bind<PlayerHpBarActor>().AsSingle().NonLazy();
            Container.Bind<SuperBulletCooldownActor>().AsSingle().NonLazy();
            Container.Bind<FuelBarActor>().AsSingle().NonLazy();
            Container.Bind<FuelReplenishActor>().AsSingle().NonLazy();
            Container.Bind<AliensTimerActor>().AsSingle().NonLazy();
        }
    }
}