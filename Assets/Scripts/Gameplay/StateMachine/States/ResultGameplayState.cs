using Application.UI;
using Application.UI.Common;
using Gameplay.Player;
using UnityEngine;
using Zenject;

namespace Gameplay.StateMachine.States
{
    public class ResultGameplayState : BaseGameplayState
    {
        private readonly IPlayerState _playerState;
        private readonly ILevelManager _levelManager;

        public ResultGameplayState(
            SignalBus signals,
            IScreenController screenController,
            IPlayerState playerState,
            ILevelManager levelManager
        ) : base(signals, screenController)
        {
            _playerState = playerState;
            _levelManager = levelManager;
        }
        public override void OnEnter()
        {
            Debug.Log($"RESULT STATE");
            _screenController.Show(GameplayScreenType.RESULT);
            
            Debug.Log($"NEXT LEVEL:{_levelManager.IsNextLevel}");
        }

        public override void OnExit()
        {
            _playerState.Hide();
            _levelManager.Clear();
        }

        public class Factory : BaseFactory
        {
        }
    }
}