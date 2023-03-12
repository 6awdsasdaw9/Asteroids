using Code.Data;
using Code.Infrastructure.Factory;
using Code.Player;

namespace Code.Services.Actors
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