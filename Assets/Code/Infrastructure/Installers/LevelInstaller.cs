using UnityEditor;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class LevelInstaller: MonoInstaller<LevelInstaller>
    {
        [SerializeField] private GameObject playerBulletPrefab;

        public override void InstallBindings()
        {
            BindItemPool();
        }


        private void BindItemPool()
        {
            /*Container.BindMemoryPool<>().WithInitialSize(10)
                .FromComponentInNewPrefab(playerBulletPrefab)
                .UnderTransformGroup("Items");
            Container.BindInterfacesTo<>().AsSingle();*/
        }
    }
}