using Code.UI;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Fuel))]
public class FuelBarActor : MonoBehaviour
{
    [SerializeField] private Fuel _fuel;
    private Bar _fuelBar;
    
    [Inject]
    private void Construct(UIDisplay hud)
    {
        _fuelBar = hud.fuelBar;
        _fuel.OnStatChanged += UpdateFuelBar;
    }
    
    private void OnDestroy()
    {
        _fuel.OnStatChanged -= UpdateFuelBar;
    }

    private void UpdateFuelBar()
    {
        _fuelBar.SetValue(_fuel.Current, _fuel.Max);
    }
}