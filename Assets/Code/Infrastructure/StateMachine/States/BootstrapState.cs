using Code.Services;

namespace Code.Infrastructure.StateMachine.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;


        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;

        }

        public void Enter()
        {
            _sceneLoader.Load(Constants.initialScene, onLoaded: EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadLevelState, string>(Constants.mainScene);
        }
    }
}