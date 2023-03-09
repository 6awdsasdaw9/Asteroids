using Code.UI;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Fuel))]
public class FuelBarActor : MonoBehaviour
{
    [SerializeField] private Fuel _fuel;
    private FuelBar _fuelFuelBar;
    
    [Inject]
    private void Construct(UIDisplay hud)
    {
        _fuelFuelBar = hud.fuelBar;
        _fuel.OnStatChanged += UpdateFuelBar;
    }
    
    private void OnDestroy()
    {
        _fuel.OnStatChanged -= UpdateFuelBar;
    }

    private void UpdateFuelBar()
    {
        _fuelFuelBar.SetValue(_fuel.Current, _fuel.Max);
    }
}