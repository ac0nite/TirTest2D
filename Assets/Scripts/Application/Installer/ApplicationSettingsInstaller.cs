using UnityEngine;
using Zenject;

namespace Application.Installer
{
    [CreateAssetMenu(fileName = "ApplicationSettingsInstaller", menuName = "Installers/ApplicationSettingsInstaller")]
    public class ApplicationSettingsInstaller : ScriptableObjectInstaller<ApplicationSettingsInstaller>
    {
        public override void InstallBindings()
        {
        }
    }
}