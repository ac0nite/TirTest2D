using UnityEngine;
using Zenject;

namespace Gameplay.Installer
{
    [CreateAssetMenu(fileName = "GameplaySettingsInstaller", menuName = "Installers/GameplaySettingsInstaller")]
    public class GameplaySettingsInstaller : ScriptableObjectInstaller<GameplaySettingsInstaller>
    {
        public override void InstallBindings()
        {
        }
    }
}