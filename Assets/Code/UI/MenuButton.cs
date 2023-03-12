using Code.Infrastructure.StateMachine;
using Code.Infrastructure.StateMachine.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI
{
    [RequireComponent(typeof(Button))]
    public class MenuButton: MonoBehaviour
    {
        private enum ButtonType
        {
            ReloadScene,
            ExitGame
        }

        [SerializeField] private ButtonType _buttonType;
        DiContainer _container;

        [Inject]
        private void Construct(DiContainer container)
        {
            _container = container;
            Button button = GetComponent<Button>();
            switch (_buttonType)
            {
                case ButtonType.ExitGame:
                    button.onClick.AddListener(ExitGame);
                    break;
                case  ButtonType.ReloadScene:
                    button.onClick.AddListener(ReloadScene);
                    break;
            }
        }

        private void ExitGame()
        {
            Application.Quit();
        }
        
        private void ReloadScene()
        {
            GameStateMachine stateMachine = _container.Resolve<GameStateMachine>();
            stateMachine.Enter<BootstrapState>();//God forgive me
        }

    }
}