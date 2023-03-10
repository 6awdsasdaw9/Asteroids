using Code.Stats;
using UnityEngine;
using Zenject;

namespace Code.UI
{
    [RequireComponent(typeof(PlayerFuel))]
    public class FuelBarActor : MonoBehaviour
    {
        [SerializeField] private PlayerFuel playerFuel;
        private FuelBar _fuelFuelBar;
    
        [Inject]
        private void Construct(UIDisplay hud)
        {
            _fuelFuelBar = hud.fuelBar;
            playerFuel.OnStatChanged += UpdatePlayerFuelBar;
        }
    
        private void OnDestroy()
        {
            playerFuel.OnStatChanged -= UpdatePlayerFuelBar;
        }

        private void UpdatePlayerFuelBar()
        {
            _fuelFuelBar.SetValue(playerFuel.Current, playerFuel.Max);
        }
    }
}