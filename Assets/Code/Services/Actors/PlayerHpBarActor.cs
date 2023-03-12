using Code.Interfaces;
using Code.Player;
using Code.UI;
using ModestTree.Util;

namespace Code.Services.Actors
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