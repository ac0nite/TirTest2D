using Application.UI;
using Application.UI.Common;
using Gameplay.Bullets;
using Gameplay.Player;
using UnityEngine;
using Zenject;

namespace Gameplay.StateMachine.States
{
    public class LoadingGameplayState : BaseGameplayState
    {
        private readonly IWeaponModelSetter _weaponModel;
        private readonly IPlayerState _playerState;

        public LoadingGameplayState(
            SignalBus signals, 
            IScreenController screenController,
            IWeaponModelSetter weaponModel,
            IPlayerState playerState)
            : base(signals, screenController)
        {
            _weaponModel = weaponModel;
            _playerState = playerState;
        }
        public override void OnEnter()
        {
            Debug.Log($"LOADING STATE");
            _screenController.Show(GameplayScreenType.LOADING);
            
            InitWeapon();
            
            _playerState.Initialise();
            _playerState.Show();
            _playerState.SetActive(true);

            _signals.Fire(new GameplayStateMachine.Signals.NextState(GameplayStateEnum.GAMEPLAY));
        }

        private void InitWeapon()
        {
            _weaponModel.BulletType = BulletType.CANNONBALL;
            _weaponModel.Level = 1;
        }

        public override void OnExit()
        {
            
        }
        
        public class Factory : BaseFactory
        {
        }
    }
}