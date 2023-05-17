using System;
using Common.StateMachine;
using Gameplay.StateMachine.States;
using Zenject;

namespace Gameplay.StateMachine
{
    public enum GameplayStateEnum
    {
        PRE_GAMEPLAY,
        LOADING,
        GAMEPLAY,
        RESULT
    }
    
    public class GameplayStateMachine : BaseStateMachine<GameplayStateEnum>, IInitializable, IDisposable
    {
        private readonly SignalBus _signals;

        public GameplayStateMachine(
            SignalBus signals,
            PreGameplayState.Factory preGameplayFactory,
            LoadingGameplayState.Factory loadingStateFactory,
            GameGameplayState.Factory gameFactory,
            ResultGameplayState.Factory resultFactory)
        {
            _signals = signals;
            _signals.Subscribe<GameplayStateMachine.Signals.NextState>(OnNextState);
            
            Register(GameplayStateEnum.PRE_GAMEPLAY, preGameplayFactory.Create()).GoesTo(GameplayStateEnum.LOADING);
            Register(GameplayStateEnum.LOADING, loadingStateFactory.Create()).GoesTo(GameplayStateEnum.GAMEPLAY);
            Register(GameplayStateEnum.GAMEPLAY, gameFactory.Create()).GoesTo(GameplayStateEnum.RESULT);
            Register(GameplayStateEnum.RESULT, resultFactory.Create()).GoesTo(GameplayStateEnum.LOADING);
        }
        
        public void Initialize()
        {
            Run(GameplayStateEnum.PRE_GAMEPLAY);
        }

        private void OnNextState(GameplayStateMachine.Signals.NextState param)
        {
            NextState(param.NextStateType);
        }
        
        public void Dispose()
        {
            _signals.TryUnsubscribe<GameplayStateMachine.Signals.NextState>(OnNextState);
        }

        #region SIGNALS

        public class Signals
        {
            public class NextState
            {
                public GameplayStateEnum NextStateType { get; private set; }
                public NextState(GameplayStateEnum nextStateType)
                {
                    NextStateType = nextStateType;
                }
            }
        }

        #endregion
    }
}