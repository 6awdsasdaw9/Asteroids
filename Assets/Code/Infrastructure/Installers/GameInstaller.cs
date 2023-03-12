using Code.Data;
using Code.Infrastructure.StateMachine;
using Code.Infrastructure.StateMachine.States;
using Code.Services;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class GameInstaller : MonoInstaller<GameInstaller>, IInitializable, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain curtain;
  
        
        public override void InstallBindings()
        {
            BindInterfaces();
            BindStateMachine();
        }

        public void Initialize() =>
            Container.Resolve<GameStateMachine>().Enter<BootstrapState>();

        private void BindInterfaces() =>
            Container.BindInterfacesTo<GameInstaller>()
                .FromInstance(this);
        
        private void BindStateMachine() =>
            Container.Bind<GameStateMachine>()
                .AsSingle().WithArguments(new SceneLoader(this), curtain).NonLazy();
    }
}