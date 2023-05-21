using Application.UI;
using Application.UI.Common;
using Gameplay.Player;
using UnityEngine;
using Zenject;

namespace Gameplay.StateMachine.States
{
    public class RunPlayGameplayState : BaseGameplayState
    {
        private readonly IPlayerState _playerState;
        private readonly ILevelManager _levelManager;

        public RunPlayGameplayState(
            SignalBus signals, 
            IScreenController screenController,
            IPlayerState playerState,
            ILevelManager levelManager) 
            : base(signals, screenController)
        {
            _playerState = playerState;
            _levelManager = levelManager;
        }
        public override void OnEnter()
        {
            Debug.Log($"GAMEPLAY STATE");
            
            _screenController.Show(GameplayScreenType.GAMEPLAY);
            _playerState.SetActive(true);
            _levelManager.Start(0);
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