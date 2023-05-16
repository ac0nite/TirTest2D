using Gameplay.Bullets;
using UnityEngine;
using Zenject;

namespace Gameplay.Installer
{
    [CreateAssetMenu(fileName = "GameplaySettingsInstaller", menuName = "Installers/GameplaySettingsInstaller")]
    public class GameplaySettingsInstaller : ScriptableObjectInstaller<GameplaySettingsInstaller>
    {
        [Header("Gameplay")] 
        [SerializeField] private GameplaySettings _gameplaySettings;
        public override void InstallBindings()
        {
            Container.BindInstance(_gameplaySettings);
        }
    }
}