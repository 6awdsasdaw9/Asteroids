using Code.Data;
using Code.Enemy;
using Code.Player;
using Code.Stats;

namespace Code.UI
{
    public class FuelReplenishActor
    {
        private readonly PlayerFuel _fuel;
        private readonly float _fuelFromAliens;

        public FuelReplenishActor(PlayerMove player, GameConfig config, EnemiesFactory enemiesFactory)
        {
            _fuel = player.GetComponent<PlayerFuel>();
            _fuelFromAliens = config.fuelFromAliens;

            enemiesFactory.OnDeathAliens += ReplenishFuel;
        }

        private void ReplenishFuel()
        {
            _fuel.Replenish(_fuelFromAliens);
        }
    }
}