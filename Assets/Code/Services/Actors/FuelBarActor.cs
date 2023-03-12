using Code.Player;
using Code.UI;

namespace Code.Services.Actors
{
    public class FuelBarActor
    { 
        private readonly PlayerFuel _playerFuel;
        private readonly FuelBar _fuelFuelBar;

        
        private  FuelBarActor(UIDisplay hud, PlayerMove player)
        {
            _fuelFuelBar = hud.fuelBar;
            _playerFuel = player.GetComponent<PlayerFuel>();
            _playerFuel.OnStatChanged = UpdatePlayerFuelBar;
        }
        
        private void UpdatePlayerFuelBar()
        {
            _fuelFuelBar.SetValue(_playerFuel.Current, _playerFuel.Max);
        }
    }
}