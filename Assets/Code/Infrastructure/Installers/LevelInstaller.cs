using Code.Data;
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
            BindItemPool();
        }


        private void BindItemPool()
        {
            Container.BindMemoryPool<Bullet, Bullet.Pool>()
                .WithInitialSize(_settings.playerBulletPoolSize)
                .ExpandByOneAtATime()
                .FromComponentInNewPrefab(_prefabs.playerBullet)
                .UnderTransformGroup("Player Bullets");
          
        }
    }
}