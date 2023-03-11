using Code.Player;
using Code.Stats;

namespace Code.UI
{
    public class PlayerHpBarActor 
    {
        private IPlayerHealth _health;
        private HpBar _hpBar;

        private PlayerHpBarActor(UIDisplay hud, PlayerMove player)
        {
            _health = player.GetComponent<IPlayerHealth>();
            _hpBar = hud.hpBar;
            _health.OnStatChanged += UpdateHealthBar;
        }
        
        private void UpdateHealthBar()
        {
            _hpBar.SetValue(_health.Current, _health.Max);
        }
    }
}