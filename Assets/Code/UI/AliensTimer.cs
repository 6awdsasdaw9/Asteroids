using System;
using Code.Enemy;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI
{
    public class AliensTimer: MonoBehaviour
    {
        [SerializeField] private Text textTimer;
        private DiContainer _container;
        private EnemiesFactory _factory;
        
        [Inject]
        private void Construct(DiContainer container)
        {
            _container = container;
        }

        private void Start()
        {
            _factory = _container.Resolve<EnemiesFactory>();
            this.UpdateAsObservable()
                .Subscribe(_ => UpdateText())
                .AddTo(this);
        }

        private void UpdateText()
        {
            textTimer.text = Mathf.Round(_factory.CurrentAliensCooldown).ToString();
        }
    }
}