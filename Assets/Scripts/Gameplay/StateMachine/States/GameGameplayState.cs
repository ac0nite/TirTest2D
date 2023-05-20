using Application.UI.Common;
using Gameplay.Player;
using UnityEngine;
using Zenject;

namespace Gameplay.StateMachine.States
{
    public class GameGameplayState : BaseGameplayState
    {
        private readonly IPlayerState _playerState;

        public GameGameplayState(
            SignalBus signals, 
            IScreenController screenController,
            IPlayerState playerState) 
            : base(signals, screenController)
        {
            _playerState = playerState;
        }
        public override void OnEnter()
        {
            Debug.Log($"GAMEPLAY STATE");
            _screenController.HideAll();
        }

        public override void OnExit()
        {
            _playerState.SetActive(false);
        }

        public class Factory : BaseFactory
        {
        }
    }
}