using Application.UI;
using Application.UI.Common;
using Gameplay.Bullets.Settings;
using Gameplay.Models;
using UnityEngine;
using Zenject;

namespace Gameplay.StateMachine.States
{
    public class PreGameplayState : BaseGameplayState
    {
        private readonly IGameplayBackground _gameplayBackground;
        private readonly IGameplayModelSetter _gameplayModel;

        public PreGameplayState(
            SignalBus signals, 
            IScreenController screenController,
            IGameplayBackground gameplayBackground,
            IGameplayModelSetter gameplayModel
            ) : base(signals, screenController)
        {
            _gameplayBackground = gameplayBackground;
            _gameplayModel = gameplayModel;
        }
        public override void OnEnter()
        {
            Debug.Log($"PRE GAMEPLAY STATE");
            
            _gameplayBackground.ScreenFill(new Vector2(0, 1.5f));
            InitDefaultLevel();
            
            _signals.Fire(new GameplayStateMachine.Signals.NextState(GameplayStateEnum.LOADING));
        }
        
        private void InitDefaultLevel()
        {
            _gameplayModel.BulletType = BulletType.CANNONBALL;
            _gameplayModel.Level = 1;
        }

        public override void OnExit()
        {
        }

        public class Factory : BaseFactory
        { }
    }
}