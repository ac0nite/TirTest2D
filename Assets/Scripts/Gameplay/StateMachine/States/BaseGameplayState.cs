using System;
using Application.UI.Common;
using Common.StateMachine;
using Zenject;

namespace Gameplay.StateMachine.States
{
    public abstract class BaseGameplayState : IState, IDisposable
    {
        protected readonly SignalBus _signals;
        protected readonly IScreenController _screenController;

        public BaseGameplayState(SignalBus signals, IScreenController screenController)
        {
            _signals = signals;
            _screenController = screenController;
        }

        public abstract void OnEnter();
        public abstract void OnExit();
        public virtual void Dispose()
        { }
        
        #region FACTORY
        
        public class BaseFactory : PlaceholderFactory<IState>
        {
        }
        
        #endregion
    }
}