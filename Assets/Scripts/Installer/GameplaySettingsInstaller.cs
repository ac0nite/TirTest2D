using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameplaySettingsInstaller", menuName = "Installers/GameplaySettingsInstaller")]
public class GameplaySettingsInstaller : ScriptableObjectInstaller<GameplaySettingsInstaller>
{
    public override void InstallBindings()
    {
    }
}