using Application.UI.Common;
using UnityEngine;
using Zenject;

namespace Gameplay.StateMachine.States
{
    public class GameGameplayState : BaseGameplayState
    {
        public GameGameplayState(
            SignalBus signals, 
            IScreenController screenController) : base(signals, screenController)
        {
        }
        public override void OnEnter()
        {
            Debug.Log($"GAMEPLAY STATE");
            _screenController.HideAll();
        }

        public override void OnExit()
        {
        }

        public class Factory : BaseFactory
        {
        }
    }
}