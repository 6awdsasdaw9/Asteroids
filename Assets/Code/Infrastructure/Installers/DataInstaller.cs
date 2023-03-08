using Code.Data;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
    [CreateAssetMenu(fileName = "DataInstaller", menuName = "ScriptableObjects/Data/DataInstaller")]
    public class DataInstaller :ScriptableObjectInstaller<DataInstaller>
    {
        [SerializeField] private GameSettings _settings;
        [SerializeField] private GamePrefabs _prefabs;
        public override void InstallBindings()
        {
            Container.BindInstance(_settings);
            Container.BindInstance(_prefabs);
        }
    }
}