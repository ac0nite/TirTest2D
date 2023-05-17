using System;
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
    
    public class ApplicationStateMachine : BaseStateMachine<ApplicationStateEnum>, IDisposable
    {
        private SignalBus _signals;
        
        public ApplicationStateMachine(
            LoadingApplicationState.Factory loadingStateFactory,
            GameplayApplicationState.Factory gameplayStateFactory,
            SignalBus signals)
        {
            Debug.Log($"Application StateMachine Init");
            
            Register(ApplicationStateEnum.LOADING, loadingStateFactory.Create()).GoesTo(ApplicationStateEnum.GAMEPLAY);
            Register(ApplicationStateEnum.GAMEPLAY, gameplayStateFactory.Create());
            
            Run(ApplicationStateEnum.LOADING);

            _signals = signals;
            _signals.Subscribe<ApplicationStateMachine.Signals.NextState>(ChangeState);
        }

        private void ChangeState(Signals.NextState arg)
        {
            NextState(arg.NextStateType);
        }
        
        public void Dispose()
        {
            _signals.TryUnsubscribe<ApplicationStateMachine.Signals.NextState>(ChangeState);
        }

        #region SIGNALS

        public class Signals
        {
            public class NextState
            {
                public ApplicationStateEnum NextStateType { get; private set; }
                public NextState(ApplicationStateEnum nextStateType)
                {
                    NextStateType = nextStateType;
                }
            }
        }

        #endregion
    }
}