using Application.UI;
using Application.UI.Common;
using UnityEngine;
using Zenject;

namespace Gameplay.StateMachine.States
{
    public class LoadingGameplayState : BaseGameplayState
    {
        public LoadingGameplayState(
            SignalBus signals, 
            IScreenController screenController) : base(signals, screenController)
        {
        }
        public override void OnEnter()
        {
            Debug.Log($"LOADING STATE");
            _screenController.Show(GameplayScreenType.LOADING);
            _signals.Fire(new GameplayStateMachine.Signals.NextState(GameplayStateEnum.GAMEPLAY));
        }

        public override void OnExit()
        {
            
        }
        
        public class Factory : BaseFactory
        {
        }
    }
}