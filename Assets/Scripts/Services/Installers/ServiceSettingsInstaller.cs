using UnityEngine;
using Zenject;

namespace Services.Installers
{
    [CreateAssetMenu(fileName = "ServiceSettingsInstaller", menuName = "Installers/ServiceSettingsInstaller")]
    public class ServiceSettingsInstaller : ScriptableObjectInstaller<ServiceSettingsInstaller>
    {
        public override void InstallBindings()
        {
        }
    }
}