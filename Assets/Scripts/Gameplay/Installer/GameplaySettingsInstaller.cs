using Gameplay.Settings;
using UnityEngine;
using Zenject;

namespace Gameplay.Installer
{
    [CreateAssetMenu(fileName = "GameplaySettingsInstaller", menuName = "Installers/GameplaySettingsInstaller")]
    public class GameplaySettingsInstaller : ScriptableObjectInstaller<GameplaySettingsInstaller>
    {
        [Header("Resources")] 
        [SerializeField] private GameplayResources _gameplayResources;
        
        [Header("Gameplay")] 
        [SerializeField] private GameplaySettings _gameplaySettings;
        public override void InstallBindings()
        {
            Container.BindInstance(_gameplayResources);
            Container.BindInstance(_gameplaySettings);
        }
    }
}