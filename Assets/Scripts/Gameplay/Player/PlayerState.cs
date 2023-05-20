using UnityEngine;

namespace Gameplay.Player
{
    public interface IPlayerState
    {
        void Initialise();
        void Show();
        void SetActive(bool active);
        void Hide();
    }
    
    public class PlayerState : IPlayerState
    {
        private readonly IPLayer _cannon;
        private readonly EnvironmentKeeper _environmentKeeper;
        private readonly Vector3 _startPosition;
        private readonly IPlayerShooting _playerShooting;
        private readonly ICannonHelper _cannonHelper;

        public PlayerState(
            CannonKeeper cannonKeeper,
            EnvironmentKeeper environmentKeeper,
            IPlayerShooting playerShooting,
            ICannonHelper cannonHelper)
        {
            _cannon = cannonKeeper.Cannon;
            _environmentKeeper = environmentKeeper;
            _startPosition = _environmentKeeper.CannonSpawnPoint.position;
            _playerShooting = playerShooting;
            _cannonHelper = cannonHelper;
        }
        
        public void Initialise()
        {
            _playerShooting.Initialise(_cannon);
            _cannonHelper.Initialise(_cannon);
        }

        public void Show()
        {
            _cannon.SetActive(true, _startPosition);
        }

        public void SetActive(bool active)
        {
            _playerShooting.SetActive(active);
            _cannonHelper.CannonTurningActive(active);
        }

        public void Hide()
        {
            _cannon.SetActive(false);
        }
    }
}