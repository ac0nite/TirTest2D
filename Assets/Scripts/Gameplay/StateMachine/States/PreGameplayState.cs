using Application.UI;
using Application.UI.Common;
using UnityEngine;
using Zenject;

namespace Gameplay.StateMachine.States
{
    public class PreGameplayState : BaseGameplayState
    {
        public PreGameplayState(
            SignalBus signals, 
            IScreenController screenController) : base(signals, screenController)
        {
            
        }
        public override void OnEnter()
        {
            Debug.Log($"PRE GAMEPLAY STATE");
            _signals.Fire(new GameplayStateMachine.Signals.NextState(GameplayStateEnum.LOADING));
        }

        public override void OnExit()
        {
        }

        public class Factory : BaseFactory
        { }
    }
}