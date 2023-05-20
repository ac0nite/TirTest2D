using Application.UI;
using Application.UI.Common;
using UnityEngine;
using Zenject;

namespace Gameplay.StateMachine.States
{
    public class PreGameplayState : BaseGameplayState
    {
        private readonly IScreenBackground _screenBackground;

        public PreGameplayState(
            SignalBus signals, 
            IScreenController screenController,
            IScreenBackground screenBackground) : base(signals, screenController)
        {
            _screenBackground = screenBackground;
        }
        public override void OnEnter()
        {
            Debug.Log($"PRE GAMEPLAY STATE");
            _screenBackground.ScreenFill(new Vector2(0, 1.5f));
            _signals.Fire(new GameplayStateMachine.Signals.NextState(GameplayStateEnum.LOADING));
        }

        public override void OnExit()
        {
        }

        public class Factory : BaseFactory
        { }
    }
}