using Application.StateMachine;
using UnityEngine;
using Zenject;

namespace Services
{
    public class GameLoader : IInitializable
    {
        private readonly SignalBus _signals;
        public GameLoader(SignalBus signals)
        {
            _signals = signals;

        }

        public void Initialize()
        {
            Debug.Log($"GameLoader Init");
            _signals.TryFire(new ApplicationStateMachine.Signals.NextState(ApplicationStateEnum.GAMEPLAY));
        }


        private void DisableLogger()
        {
#if UNITY_EDITOR
            UnityEngine.Debug.unityLogger.filterLogType = UnityEngine.LogType.Log;
#else
            UnityEngine.Debug.unityLogger.filterLogType = UnityEngine.LogType.Exception;
#endif
        }
    }
}