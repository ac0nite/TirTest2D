using System;
using Application.UI;
using Application.UI.Common;
using Gameplay.Bullets;
using Gameplay.Bullets.Settings;
using Gameplay.Models;
using Gameplay.Player;
using UnityEngine;
using Zenject;

namespace Gameplay.StateMachine.States
{
    public class LoadingGameplayState : BaseGameplayState
    {
        private readonly IGameplayModelSetter _gameplayModel;
        private readonly IPlayerState _playerState;
        private readonly ILevelManager _levelManager;

        public LoadingGameplayState(
            SignalBus signals, 
            IScreenController screenController,
            IGameplayModelSetter gameplayModel,
            IPlayerState playerState,
            ILevelManager levelManager)
            : base(signals, screenController)
        {
            _gameplayModel = gameplayModel;
            _playerState = playerState;
            _levelManager = levelManager;
        }
        public override void OnEnter()
        {
            Debug.Log($"LOADING STATE");
            _screenController.Show(GameplayScreenType.LOADING);

            if (_levelManager.IsNextLevel)
            {
                Debug.Log($"MAYBE NEXT LEVEL [{_gameplayModel.Level + 1}]");
                _gameplayModel.Level++;
            }

            _playerState.Initialise();

            _signals.Fire(new GameplayStateMachine.Signals.NextState(GameplayStateEnum.PREVIEW));
        }

        public override void OnExit()
        {
            
        }

        public class Factory : BaseFactory
        {
        }
    }
}