using Code.Player;
using Code.UI;

namespace Code.Services.Actors
{
    public class SuperBulletCooldownActor 
    {
        private readonly PlayerAttack _playerAttack;
        private readonly SuperBulletCooldownBar _superBulletCooldownBar;
        

        private SuperBulletCooldownActor(UIDisplay hud, PlayerMove player)
        {
            _playerAttack = player.GetComponent<PlayerAttack>();
            _superBulletCooldownBar = hud.superBulletCooldownBar;
            _playerAttack.SuperCooldownChanged += UpdateSuperBulletBar;
        }
    
        private void OnDestroy()
        {
            _playerAttack.SuperCooldownChanged -= UpdateSuperBulletBar;
        }

        private void UpdateSuperBulletBar(float currentCooldown,float cooldown)
        {
            
            _superBulletCooldownBar.SetValue(currentCooldown,cooldown);
        }
    }
}