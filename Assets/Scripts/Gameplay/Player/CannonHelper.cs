using Gameplay.Input;
using UnityEngine;
using Zenject;

namespace Gameplay.Player
{
    public interface ICannonTurning : ITickable
    {
        void CannonTurningActive(bool active);
    }

    public interface ICannonHelper : ICannonTurning
    {
        void Initialise(IPLayer player);
    }
    
    public class CannonHelper : ICannonHelper, ICannonTurning
    {
        private readonly IInputService _inputService;
        private IPLayer _player;
        private bool _isActive;
        private Vector2 _screenPosition;
        private readonly Camera _camera;

        public CannonHelper(
            Camera camera,
            IInputService inputService)
        {
            _camera = camera;
            _inputService = inputService;
        }
        
        public void Initialise(IPLayer player)
        {
            _player = player;
        }
        
        public void CannonTurningActive(bool active)
        {
            _isActive = active;
        }

        public void Tick()
        {
            if (CanTurning)
                MuzzleTurning();
        }

        private bool CanTurning => _isActive && _inputService.GetPressTouch(out _screenPosition);
        
        private void MuzzleTurning()
        {
            _player.MuzzleTransform.rotation = LookAt2D(_screenPosition);
        }
        
        private Quaternion LookAt2D(Vector3 screenPosition)
        {
            var worldPosition = _camera.ScreenToWorldPoint(screenPosition);
            var direction = worldPosition - _player.Transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            return Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}