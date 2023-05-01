
using Application.StateMachine;
using UnityEngine;
using Zenject;

namespace Loading
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
            Debug.Log($"GameLoader:Initialize");
            _signals.TryFire(new ApplicationStateMachine.Signals.OnState(ApplicationStateEnum.GAMEPLAY));
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