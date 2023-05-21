using Application.UI;
using Application.UI.Common;
using Gameplay.Player;
using UnityEngine;
using Zenject;

namespace Gameplay.StateMachine.States
{
    public class PreviewGameplayState : BaseGameplayState
    {
        private readonly IPlayerState _playerState;
        private readonly ILevelManager _levelManager;

        public PreviewGameplayState(
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
            Debug.Log($"PREVIEW STATE");
            _levelManager.Initialise();
            _screenController.Show(GameplayScreenType.PREVIEW);
            _playerState.Show();
        }

        public override void OnExit()
        {
            
        }
        
        public class Factory : BaseFactory
        { }
    }
}