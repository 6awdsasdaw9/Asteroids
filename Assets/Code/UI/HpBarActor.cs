using Code.Player;
using UnityEngine;
using Zenject;

namespace Code.UI
{
    public class HpBarActor : MonoBehaviour
    {
        [SerializeField] private PlayerHealth _health;
        private HpBar _hpBar;
    
        [Inject]
        private void Construct(UIDisplay hud)
        {
            _hpBar = hud.hpBar;
            _health.OnStatChanged += UpdateHealthBar;
        }
    
        private void OnDestroy()
        {
            _health.OnStatChanged -= UpdateHealthBar;
        }

        private void UpdateHealthBar()
        {
            _hpBar.SetValue(_health.Current, _health.Max);
        }
    }
}