using Code.Infrastructure.StateMachine;
using Code.Infrastructure.StateMachine.States;
using Code.Logic;
using Code.Services;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class GameInstaller : MonoInstaller<GameInstaller>, IInitializable, ICoroutineRunner
    {
        public LoadingCurtain curtain;
        
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