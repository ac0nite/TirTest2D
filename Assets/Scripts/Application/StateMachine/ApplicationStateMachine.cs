using Application.StateMachine.States;
using Common.StateMachine;
using UnityEngine;
using Zenject;

namespace Application.StateMachine
{
    public enum ApplicationStateEnum
    {
        LOADING,
        GAMEPLAY
    }
    
    public class ApplicationStateMachine : BaseStateMachine<ApplicationStateEnum>, IInitializable
    {
        private SignalBus _signals;
        
        public ApplicationStateMachine(
            LoadingState.Factory loadingStateFactory,
            GameplayState.Factory gameplayStateFactory,
            SignalBus signals)
        {
            Register(ApplicationStateEnum.LOADING, loadingStateFactory.Create())
                .GoesTo(ApplicationStateEnum.GAMEPLAY);
            
            Register(ApplicationStateEnum.GAMEPLAY, gameplayStateFactory.Create());
            
            Run(ApplicationStateEnum.LOADING);

            _signals = signals;
            _signals.Subscribe<ApplicationStateMachine.Signals.OnState>(ChangeState);
        }
        
        public void Initialize()
        {
            Debug.Log($"ApplicationStateMachine Initializable");
        }

        private void ChangeState(Signals.OnState arg)
        {
            NextState(arg.StateEnum);
        }

        #region SIGNALS

        public class Signals
        {
            public class OnState
            {
                public readonly ApplicationStateEnum StateEnum;

                public OnState(ApplicationStateEnum stateEnum)
                {
                    StateEnum = stateEnum;
                }
            }
        }

        #endregion
    }
}